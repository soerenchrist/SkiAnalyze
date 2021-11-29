using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkiAnalyze.ApiModels;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Common.Analysis.Specifications;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.Core.SessionAggregate.Specifications;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisResult : BaseAsyncEndpoint
    .WithRequest<GetAnalysisResultRequest>
    .WithResponse<AnalysisResultDto>
{
    private readonly IReadRepository<AnalysisStatus> _statusRepository;
    private readonly IReadRepository<UserSession> _sessionRepository;
    private readonly IMapper _mapper;

    public GetAnalysisResult(IReadRepository<AnalysisStatus> statusRepository,
        IReadRepository<UserSession> sessionRepository,
        IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
        _statusRepository = statusRepository;
    }

    [HttpGet("/api/analysis/result")]
    public override async Task<ActionResult<AnalysisResultDto>> HandleAsync([FromQuery] GetAnalysisResultRequest request, CancellationToken cancellationToken = default)
    {
        var running = await _statusRepository.GetBySpecAsync(new RunningAnalysisBySession(request.UserSessionId));
        if (running != null)
            return BadRequest("There is a currently running analysis");

        var session = await _sessionRepository.GetBySpecAsync(new GetCompleteSession(request.UserSessionId));
        if (session == null)
            return NotFound();

        var runs = session.Tracks.SelectMany(x => x.Runs);
        var runDtos = _mapper.Map<List<RunDto>>(runs);
        var dto = new AnalysisResultDto
        {
            Runs = runDtos,
            Bounds = runDtos.SelectMany(x => x.Coordinates)
                .Select(x => (ICoordinate)x)
                .GetBounds()
        };
        return Ok(dto);
    }
}
