namespace SkiAnalyze.ApiEndpoints.StatsEndpoints.Totals;

public class GetTimelineRequest
{
    [FromQuery] public string ByProperty { get; set; } = "distance";
    [FromQuery] public string DateRange { get; set; } = "year";
    [FromQuery] public int Year { get; set; } = DateTime.Now.Year;
}