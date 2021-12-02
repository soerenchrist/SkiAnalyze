using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class GetDetails : BaseAsyncEndpoint
    .WithRequest<GetDetailsRequest>
    .WithResponse<SkiAreaDetailDto>
{
    private readonly IReadRepository<SkiArea> _areaRepository;
    private readonly IMapper _mapper;

    public GetDetails(IReadRepository<SkiArea> areaRepository,
        IMapper mapper)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/skiareas/{skiAreaId}")]
    public override async Task<ActionResult<SkiAreaDetailDto>> HandleAsync([FromRoute] GetDetailsRequest request, CancellationToken cancellationToken = default)
    {
        var area = await _areaRepository.GetBySpecAsync(new GetSkiAreaWithNodesSpec(request.SkiAreaId));
        if (area == null)
            return NotFound();

        var dto = _mapper.Map<SkiAreaDetailDto>(area);
        return dto;
    }
}
