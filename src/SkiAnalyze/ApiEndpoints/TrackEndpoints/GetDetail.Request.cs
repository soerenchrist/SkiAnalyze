namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class GetDetailRequest
{
    [FromRoute] public int TrackId { get; set; }
}
