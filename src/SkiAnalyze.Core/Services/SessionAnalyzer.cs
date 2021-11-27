using Ardalis.Result;
using NetTopologySuite.IO;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.Services.MapMatching;
using SkiAnalyze.Core.Util;
using System.Xml;

namespace SkiAnalyze.Core.Services;

public class SessionAnalyzer : ISessionAnalyzer
{
    private readonly IUserSessionManager _userSessionManager;
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly IPisteSearchService _pisteSearchService;
    public SessionAnalyzer(IUserSessionManager userSessionManager,
        IGondolaSearchService gondolaSearchService,
        IPisteSearchService pisteSearchService)
    {
        _userSessionManager = userSessionManager;
        _gondolaSearchService = gondolaSearchService;
        _pisteSearchService = pisteSearchService;
    }

    public async Task<Result<AnalysisResult>> StartAnalysis(Guid userSessionId)
    {
        var session = await _userSessionManager.GetUserSession(userSessionId);
        if (session == null)
            return Invalid("Invalid session id");


        var tracks = session.Tracks.ToList();
        if (tracks.Count == 0)
            return Invalid("No tracks yet!");

        var fileLoader = new GpxFileLoader();
        var gpxFiles = fileLoader.LoadGpxFiles(tracks);

        var trackPoints = gpxFiles.ToTrackPoints();
        var bounds = trackPoints.Select(x => (ICoordinate) x).GetBounds();

        var pistes = await _pisteSearchService.GetPistesInBounds(bounds);
        var gondolas = await _gondolaSearchService.GetGondolasInBounds(bounds);

        var matcher = new MatchingService();
        var runs = matcher.Match(gondolas, pistes, trackPoints.ToList());

        return new AnalysisResult
        {
            IsRunning = true,
            Bounds = bounds,
            Runs = runs.ToList()
        };
    }

    private Result<AnalysisResult> Invalid(string message)
    {
        return Result<AnalysisResult>.Invalid(GetValidationErrors(message));
    }

    private List<ValidationError> GetValidationErrors(string message)
    {
        return new List<ValidationError>()
        {
            new ValidationError
            {
                ErrorMessage = message
            }
        };
    }
}
