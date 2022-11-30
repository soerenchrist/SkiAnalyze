using System.Diagnostics;
using Microsoft.Extensions.Logging;
using SkiAnalyze.Application.Services.FileStrategies;
using SkiAnalyze.Application.Services.MapMatching;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;
using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Entities.TrackAggregate.Specifications;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Application.Services;

public class Analyzer : IAnalyzer
{
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly ILogger<Analyzer> _logger;
    private readonly IPisteSearchService _pisteSearchService;
    private readonly IRepository<AnalysisStatus> _statusRepository;
    private readonly IReadRepository<SkiArea> _skiAreaRepository;
    private readonly IRepository<Track> _tracksRepository;
    public Analyzer(IRepository<Track> tracksRepository,
        IRepository<AnalysisStatus> statusRepository,
        IReadRepository<SkiArea> skiAreaRepository,
        IGondolaSearchService gondolaSearchService,
        ILogger<Analyzer> logger,
        IPisteSearchService pisteSearchService)
    {
        _statusRepository = statusRepository;
        _skiAreaRepository = skiAreaRepository;
        _tracksRepository = tracksRepository;
        _gondolaSearchService = gondolaSearchService;
        _logger = logger;
        _pisteSearchService = pisteSearchService;
    }

    public async Task AnalyzeTrack(int trackId)
    {
        var status = new AnalysisStatus
        {
            TrackId = trackId,
            IsFinished = false,
            Success = false,
        };
        var track = await _tracksRepository.FirstOrDefaultAsync(new GetTrackWithStatusSpec(trackId));
        if (track == null)
        {
            _logger.LogInformation("No track with id {Id} found", trackId);
            return;
        }
        if (!track.AnalysisStatus?.IsFinished ?? false)
        {
            _logger.LogInformation("There is already an analysis running for track id {TrackId}", trackId);
            return;
        }
        try
        {
            await _statusRepository.AddAsync(status);

            _logger.LogInformation("Starting analyzis of track {TrackId}", trackId);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var strategy = GetStrategy(track);
            var stream = new MemoryStream(track.FileContents);

            var fileReadResult = strategy.ReadFileContents(stream);

            track.Name = fileReadResult.TrackName;

            var runs = fileReadResult.Runs;
            var trackPoints = runs.ToTrackPoints().ToList();
            var matcher = new MatchingService();

            var bounds = trackPoints.Select(x => (ICoordinate)x).GetBounds();
            var pistes = await _pisteSearchService.GetPistesInBounds(bounds);
            var gondolas = await _gondolaSearchService.GetGondolasInBounds(bounds);

            runs = matcher.Match(gondolas, pistes, runs).ToList();
            track.SetRuns(runs.ToList());

            track.UpdateRunNumbers();
            track.UpdateStats();
            track.SkiArea = await GetSkiAreaForTrack(trackPoints.ToList(), bounds);
            await _tracksRepository.UpdateAsync(track);

            stopwatch.Stop();
            _logger.LogInformation("Finished analysis of track {TrackId}. Took {Seconds}", trackId, stopwatch.Elapsed.TotalSeconds);

            status.IsFinished = true;
            status.Success = true;
            await _statusRepository.UpdateAsync(status);
        } catch(Exception ex)
        {
            status.IsFinished = true;
            status.ErrorMessage = ex.Message;
            status.Success = false;
            await _statusRepository.UpdateAsync(status);
            _logger.LogError(ex, "Error while analyzing track {TrackId}", trackId);
        }
    }


    private ITrackFileParserStrategy GetStrategy(Track track)
    {
        return track.FileType switch
        {
            TrackFileType.Fit => new FitFileParserStrategy(),
            _ => new GpxFileParserStrategy()
        };
    }

    private async Task<SkiArea?> GetSkiAreaForTrack(List<TrackPoint> trackPoints, Bounds bounds)
    {
        var results = await _skiAreaRepository.ListAsync(new GetSkiAreasPaginatedSpec(bounds));
        if (results.Count == 0)
            return null;
        if (results.Count == 1)
            return results[1];

        var random = new Random();
        foreach (var result in results)
        {
            var areaWithNodes = await _skiAreaRepository.FirstOrDefaultAsync(new GetSkiAreaWithNodesSpec(result.Id));
            if (areaWithNodes == null) continue;
            // Check some random points in the track
            var allInBounds = true;
            for (int i = 0; i < 3; i++)
            {
                var index = random.Next(0, trackPoints.Count);
                var indexIsInBounds = areaWithNodes.Nodes
                    .ToList<ICoordinate>()
                    .ContainsPoint(trackPoints[index]);
                if (!indexIsInBounds)
                {
                    allInBounds = false;
                    break;
                }
            }
            if (allInBounds)
                return result;
        }
        return null;
    }
}
