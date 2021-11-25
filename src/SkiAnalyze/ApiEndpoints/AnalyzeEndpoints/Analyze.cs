using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class Analyze : BaseAsyncEndpoint
    .WithRequest<AnalyzeRequest>
    .WithResponse<AnalysisResult>
{

    private readonly ISessionAnalyzer _sessionAnalyzer;
    public Analyze(ISessionAnalyzer sessionAnalyzer)
    {
        _sessionAnalyzer = sessionAnalyzer;
    }

    [HttpPost("/api/analysis/start")]
    [SwaggerOperation(
        Summary = "Start an analysis",
        Description = "Start the analysis of a given session",
        OperationId = "Analysis.Start",
        Tags = new[] { "AnalysisEndpoints" })
    ]
    public override async Task<ActionResult<AnalysisResult>> HandleAsync(AnalyzeRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _sessionAnalyzer.StartAnalysis(request.UserSessionId);
        return this.ToActionResult(result);
    }
}
