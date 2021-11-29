using Microsoft.Extensions.Logging;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Common.Analysis.Specifications;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.Services.MapMatching;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel.Interfaces;
using System.Diagnostics;

namespace SkiAnalyze.Core.Services;

public class SessionAnalyzer : ISessionAnalyzer
{
    private readonly IUserSessionManager _userSessionManager;
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly ILogger<SessionAnalyzer> _logger;
    private readonly IPisteSearchService _pisteSearchService;
    private readonly IRepository<AnalysisStatus> _statusRepository;
    public SessionAnalyzer(IUserSessionManager userSessionManager,
        IRepository<AnalysisStatus> statusRepository,
        IGondolaSearchService gondolaSearchService,
        ILogger<SessionAnalyzer> logger,
        IPisteSearchService pisteSearchService)
    {
        _statusRepository = statusRepository;
        _userSessionManager = userSessionManager;
        _gondolaSearchService = gondolaSearchService;
        _logger = logger;
        _pisteSearchService = pisteSearchService;
    }

    public async Task AnalyzeSession(Guid userSessionId, Guid analysisId)
    {
        var status = new AnalysisStatus
        {
            Id = analysisId,
            IsFinished = false,
            Success = false,
            UserSessionId = userSessionId
        };
        var runningAnalysis = await _statusRepository.GetBySpecAsync(new RunningAnalysisBySession(userSessionId));
        if (runningAnalysis != null)
        {
            _logger.LogInformation("There is already an analysis running for session id {SessionId}", userSessionId);
            return;
        }
        try
        {
            await _statusRepository.AddAsync(status);

            var session = await _userSessionManager.GetUserSession(userSessionId);
            if (session == null)
                throw new ArgumentException($"No session found for id {userSessionId}");

            var tracks = session.Tracks.ToList();
            if (tracks.Count == 0)
                throw new ArgumentException($"The session {userSessionId} does not have any tracks");


            _logger.LogInformation("Starting analyzis of session {SessionId}", userSessionId);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var fileLoader = new GpxFileLoader();
            var gpxFiles = fileLoader.LoadGpxFiles(tracks);

            var matcher = new MatchingService();

            var allTrackPoints = gpxFiles.ToTrackPoints();
            var bounds = allTrackPoints.Select(x => (ICoordinate)x).GetBounds();
            var pistes = await _pisteSearchService.GetPistesInBounds(bounds);
            var gondolas = await _gondolaSearchService.GetGondolasInBounds(bounds);

            var index = 0;
            foreach (var gpxFile in gpxFiles)
            {
                var track = tracks[index];
                var trackPoints = gpxFile.ToTrackPoints();
                var runs = matcher.Match(gondolas, pistes, trackPoints.ToList());
                foreach (var run in runs)
                {
                    run.Color = track.HexColor;
                }

                var result = new AnalysisResult
                {
                    Id = analysisId,
                    TrackId = track.Id,
                    Bounds = bounds
                };
                result.AddRuns(runs);

                track.AnalysisResult = result;

                index++;
            }
            stopwatch.Stop();
            _logger.LogInformation("Finished analyzis of session {SessionId}. Took {Seconds}.", userSessionId, stopwatch.Elapsed.TotalSeconds);

            await _userSessionManager.UpdateUserSession(session);
            status.IsFinished = true;
            status.Success = true;
            await _statusRepository.UpdateAsync(status);
        } catch(Exception ex)
        {
            status.IsFinished = true;
            status.ErrorMessage = ex.Message;
            status.Success = false;
            await _statusRepository.UpdateAsync(status);
            _logger.LogError(ex, "Error while analyzing {SessionId}", userSessionId);
        }
    }
}
