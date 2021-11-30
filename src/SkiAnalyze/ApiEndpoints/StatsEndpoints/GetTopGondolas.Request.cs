namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetTopGondolasRequest
{
    [FromRoute] public int TrackId { get; set; }
}
