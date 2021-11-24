using SkiAnalyze.ApiModels;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class GetTracksResponse
{
    public List<TrackDto> Tracks { get; }
    public Guid UserSessionId { get; }

    public GetTracksResponse(List<TrackDto> tracks, Guid userSessionId)
    {
        Tracks = tracks;
        UserSessionId = userSessionId;
    }
}
