namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class StartAnalysisRequest
{
    [FromRoute] public int TrackId { get; set; }
}
