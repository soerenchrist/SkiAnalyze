using Microsoft.Extensions.Logging;
using SkiAnalyze.Core.TrackAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.Services.MapMatching;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel.Interfaces;
using System.Diagnostics;
using SkiAnalyze.Core.TrackAggregate.Specifications;

namespace SkiAnalyze.Core.Services;

public class Analyzer : IAnalyzer
{
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly ILogger<Analyzer> _logger;
    private readonly IPisteSearchService _pisteSearchService;
    private readonly IRepository<AnalysisStatus> _statusRepository;
    private readonly IRepository<Track> _tracksRepository;
    public Analyzer(IRepository<Track> tracksRepository,
        IRepository<AnalysisStatus> statusRepository,
        IGondolaSearchService gondolaSearchService,
        ILogger<Analyzer> logger,
        IPisteSearchService pisteSearchService)
    {
        _statusRepository = statusRepository;
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
            }

            track.Runs = runs.ToList();

            await _tracksRepository.UpdateAsync(track);

            stopwatch.Stop();
            _logger.LogInformation("Finished analyzis of track {TrackId}. Took {Seconds}.", trackId, stopwatch.Elapsed.TotalSeconds);

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
}
