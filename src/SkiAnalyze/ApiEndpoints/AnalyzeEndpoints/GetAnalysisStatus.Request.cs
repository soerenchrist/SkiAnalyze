namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisStatusRequest
{
    [FromRoute] public int TrackId { get; set; }
}
