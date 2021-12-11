using SkiAnalyze.Core.Entities.GondolaAggregate.Specifications;
using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class GetGondolas : BaseAsyncEndpoint
    .WithRequest<GetGondolasRequest>
    .WithResponse<List<GondolaDto>>
{
    private readonly IReadRepository<SkiArea> _skiAreaRepository;
    private readonly IReadRepository<Gondola> _gondolaRepository;
    private readonly IMapper _mapper;

    public GetGondolas(IReadRepository<SkiArea> skiAreaRepository,
        IReadRepository<Gondola> gondolaRepository,
        IMapper mapper)
    {
        _skiAreaRepository = skiAreaRepository;
        _gondolaRepository = gondolaRepository;
        _mapper = mapper;
    }


    [HttpGet("/api/skiareas/{skiAreaId}/gondolas")]
    public override async Task<ActionResult<List<GondolaDto>>> HandleAsync([FromRoute] GetGondolasRequest request, CancellationToken cancellationToken = default)
    {
        var skiArea = await _skiAreaRepository.GetBySpecAsync(new GetSkiAreaWithNodesSpec(request.SkiAreaId));
        if (skiArea == null)
            return NotFound();

        var bounds = skiArea.Nodes.GetBounds();
        var gondolas = await _gondolaRepository.ListAsync(new GondolasInBoundsSpec(bounds));

        var results = new List<Gondola>();
        var polygon = skiArea.Nodes.ToList<ICoordinate>();
        // check if the gondolas start and endpoint is in bounds
        foreach(var gondola in gondolas)
        {
            var start = gondola.Coordinates.First();
            var end = gondola.Coordinates.Last();

            if (!polygon.ContainsPoint(start) || !polygon.ContainsPoint(end))
                continue;
            results.Add(gondola);
        }

        var dtos = _mapper.Map<List<GondolaDto>>(results);
        return Ok(dtos);
    }
}
