namespace SkiAnalyze.ApiEndpoints.StatsEndpoints.Totals;

public class GetTimelineRequest
{
    [FromQuery] public DateTime? EndDate { get; set; }
    [FromQuery] public DateTime? StartDate { get; set; }
    [FromQuery] public string ByProperty { get; set; } = "distance";
}