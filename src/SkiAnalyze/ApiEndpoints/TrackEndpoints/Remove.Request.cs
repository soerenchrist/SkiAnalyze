namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class RemoveTrackRequest
{
    [FromRoute] public int TrackId { get; set; }
}
