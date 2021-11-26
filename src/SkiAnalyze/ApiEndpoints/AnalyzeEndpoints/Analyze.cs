using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class Analyze : BaseAsyncEndpoint
    .WithRequest<AnalyzeRequest>
    .WithResponse<AnalysisResultDto>
{

    private readonly ISessionAnalyzer _sessionAnalyzer;
    private readonly IMapper _mapper;

    public Analyze(ISessionAnalyzer sessionAnalyzer,
        IMapper mapper)
    {
        _sessionAnalyzer = sessionAnalyzer;
        _mapper = mapper;
    }

    [HttpPost("/api/analysis/start")]
    [SwaggerOperation(
        Summary = "Start an analysis",
        Description = "Start the analysis of a given session",
        OperationId = "Analysis.Start",
        Tags = new[] { "AnalysisEndpoints" })
    ]
    public override async Task<ActionResult<AnalysisResultDto>> HandleAsync(AnalyzeRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _sessionAnalyzer.StartAnalysis(request.UserSessionId);
        var dto = ToDto(result);
        return this.ToActionResult(dto);
    }

    private Result<AnalysisResultDto> ToDto(Result<AnalysisResult> result)
    {
        if (result.IsSuccess)
        {
            var dto = _mapper.Map<AnalysisResultDto>(result.Value);
            return Result<AnalysisResultDto>.Success(dto);
        }

        return Result<AnalysisResultDto>.Invalid(result.ValidationErrors);
    }
}
