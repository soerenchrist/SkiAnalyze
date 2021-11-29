namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisStatusRequest
{
    public Guid UserSessionId { get; set; }
    public Guid AnalysisId { get; set; }
}
