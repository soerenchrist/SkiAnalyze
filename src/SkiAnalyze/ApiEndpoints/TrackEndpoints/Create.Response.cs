using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.SessionAggregate;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class CreateTrackResponse
{
    public TrackDto Track { get; set; }
    public Guid UserSessionId { get; set; }

    public CreateTrackResponse(TrackDto track, Guid userSessionId)
    {
        Track = track;
        UserSessionId = userSessionId;
    }
}
