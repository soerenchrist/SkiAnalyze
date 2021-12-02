using Microsoft.Extensions.Logging;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.Services.MapMatching;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel.Interfaces;
using System.Diagnostics;
using SkiAnalyze.Core.Entities.TrackAggregate.Specifications;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

namespace SkiAnalyze.Core.Services;

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
        var track = await _tracksRepository.GetBySpecAsync(new GetTrackWithStatusSpec(trackId));
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

            var fileLoader = new GpxFileLoader();
            var gpxFile = fileLoader.LoadGpxFile(track);

            var matcher = new MatchingService();
            var trackPoints = gpxFile.ToTrackPoints();

            var bounds = trackPoints.Select(x => (ICoordinate)x).GetBounds();
            var pistes = await _pisteSearchService.GetPistesInBounds(bounds);
            var gondolas = await _gondolaSearchService.GetGondolasInBounds(bounds);

            var runs = matcher.Match(gondolas, pistes, trackPoints.ToList());
            foreach (var run in runs)
            {
                run.Color = track.HexColor;

                if (run.Coordinates.Count < 2)
                    continue;
                run.TotalDistance = run.Coordinates
                    .Select(x => (ICoordinate)x)
                    .ToList()
                    .GetLength();
                run.TotalElevation = GetTotalElevation(run);
                run.MaxSpeed = run.Coordinates.GetMaxSpeed();
                run.AverageSpeed = run.Coordinates.GetAverageSpeed();
                run.Start = run.Coordinates.First().DateTime;
                run.End = run.Coordinates.Last().DateTime;
            }

            track.Runs = runs.ToList();
            track.TotalDistance = track.Runs.Where(x => x.Downhill).Sum(x => x.TotalDistance);
            track.TotalElevation = track.Runs.Where(x => x.Downhill).Sum(x => x.TotalElevation);
            track.MaxSpeed = track.Runs.Max(x => x.MaxSpeed);
            track.Start = track.Runs.First().Start;
            track.End = track.Runs.Last().End;
            track.Date = track.Runs.First().Start.Date;
            track.SkiArea = await GetSkiAreaForTrack(trackPoints.ToList(), bounds);
            await _tracksRepository.UpdateAsync(track);

            stopwatch.Stop();
            _logger.LogInformation("Finished analysis of track {TrackId}. Took {Seconds}.", trackId, stopwatch.Elapsed.TotalSeconds);

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

    private async Task<SkiArea?> GetSkiAreaForTrack(List<TrackPoint> trackPoints, Bounds bounds)
    {
        var results = await _skiAreaRepository.ListAsync(new GetSkiAreasInBoundsSpec(bounds));
        if (results.Count == 0)
            return null;
        if (results.Count == 1)
            return results[1];

        var random = new Random();
        foreach (var result in results)
        {
            var areaWithNodes = await _skiAreaRepository.GetBySpecAsync(new GetSkiAreaWithNodesSpec(result.Id));
            if (areaWithNodes == null) continue;
            // Check some random points in the track
            var allInBounds = true;
            for (int i = 0; i < 3; i++)
            {
                var index = random.Next(0, trackPoints.Count);
                var indexIsInBounds = IsPointInPolygon(areaWithNodes.Nodes, trackPoints[index]);
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
    public static bool IsPointInPolygon(List<SkiAreaNode> polygon, ICoordinate testPoint)
    {
        bool result = false;
        int j = polygon.Count() - 1;
        for (int i = 0; i < polygon.Count(); i++)
        {
            if (polygon[i].Latitude < testPoint.Latitude && polygon[j].Latitude >= testPoint.Latitude || polygon[j].Latitude < testPoint.Latitude && polygon[i].Latitude >= testPoint.Latitude)
            {
                if (polygon[i].Longitude + (testPoint.Latitude - polygon[i].Latitude) / (polygon[j].Latitude - polygon[i].Latitude) * (polygon[j].Longitude - polygon[i].Longitude) < testPoint.Longitude)
                {
                    result = !result;
                }
            }
            j = i;
        }
        return result;
    }

    public double GetTotalElevation(Run run)
    {
        var last = run.Coordinates.Last();
        var first = run.Coordinates.First();
        return last.Elevation - first.Elevation ?? 0;
    }
}
