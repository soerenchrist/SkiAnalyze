using Microsoft.AspNetCore.Mvc;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisResultRequest
{
    [FromRoute] public int TrackId { get; set; }
}
