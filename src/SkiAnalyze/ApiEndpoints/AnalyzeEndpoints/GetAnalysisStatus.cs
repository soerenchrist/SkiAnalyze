using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Common.Analysis.Specifications;
using SkiAnalyze.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisStatus : BaseAsyncEndpoint
    .WithRequest<GetAnalysisStatusRequest>
    .WithResponse<AnalysisStatusDto>
{
    private readonly IReadRepository<AnalysisStatus> _statusRepository;
    private readonly IMapper _mapper;
    public GetAnalysisStatus(IReadRepository<AnalysisStatus> statusRepository,
        IMapper mapper)
        => (_statusRepository, _mapper) = (statusRepository, mapper);

    [HttpGet("/api/analysis/status")]
    [SwaggerOperation(
        Summary = "Get the status of an analysis",
        Description = "Check if an analysis is still running or is finished",
        OperationId = "Analysis.Status",
        Tags = new[] { "AnalysisEndpoints" })
    ]
    public override async Task<ActionResult<AnalysisStatusDto>> HandleAsync([FromQuery] GetAnalysisStatusRequest request, CancellationToken cancellationToken = default)
    {
        var analysis = await _statusRepository.GetBySpecAsync(new AnalysisBySessionAndId(request.UserSessionId, request.AnalysisId));
        if (analysis == null)
            return NotFound();

        var dto = _mapper.Map<AnalysisResultDto>(analysis);
        return Ok(dto);
    }
}
