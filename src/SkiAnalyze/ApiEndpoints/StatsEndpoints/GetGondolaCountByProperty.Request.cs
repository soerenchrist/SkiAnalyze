namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetGondolaCountByPropertyRequest
{
    [FromRoute] public int TrackId { get; set; }
    [FromQuery] public string PropertyName { get; set; } = "type";
}
