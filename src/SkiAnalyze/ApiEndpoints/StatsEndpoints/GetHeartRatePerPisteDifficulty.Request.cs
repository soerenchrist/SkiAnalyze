namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetHeartRatePerPisteDifficultyRequest
{
    [FromRoute] public int TrackId { get; set; }
}
