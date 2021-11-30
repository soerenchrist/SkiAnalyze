namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetTopGondolaTypesRequest
{
    [FromRoute] public int TrackId { get; set; }
}
