namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetDifficultyStatsRequest
{
    [FromRoute] public int TrackId { get; set; }
}
