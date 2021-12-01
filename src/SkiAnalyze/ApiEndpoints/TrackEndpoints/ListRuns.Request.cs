namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class ListRunsRequest
{
    [FromRoute] public int TrackId { get; set; }
}
