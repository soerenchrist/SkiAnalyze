namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class RemoveTrackRequest
{
    public Guid UserSessionId { get; set; }
    public int TrackId { get; set; }
}
