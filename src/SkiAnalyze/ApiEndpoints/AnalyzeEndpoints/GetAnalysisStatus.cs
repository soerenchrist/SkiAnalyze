using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisStatus : BaseAsyncEndpoint
    .WithRequest<GetAnalysisStatusRequest>
    .WithResponse<AnalysisStatusDto>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;
    public GetAnalysisStatus(IReadRepository<Track> tracksRepository,
        IMapper mapper)
        => (_tracksRepository, _mapper) = (tracksRepository, mapper);

    [HttpGet("/api/tracks/{trackId:int}/analysis/status")]
    [SwaggerOperation(
        Summary = "Get the status of an analysis",
        Description = "Check if an analysis is still running or is finished",
        OperationId = "Analysis.Status",
        Tags = new[] { "AnalysisEndpoints" })
    ]
    public override async Task<ActionResult<AnalysisStatusDto>> HandleAsync([FromRoute] GetAnalysisStatusRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.GetBySpecAsync(new GetTrackWithStatusSpec(request.TrackId));
        if (track?.AnalysisStatus == null)
            return NotFound();

        var dto = _mapper.Map<AnalysisStatusDto>(track.AnalysisStatus);
        return Ok(dto);
    }
}
