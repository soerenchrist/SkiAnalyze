using SkiAnalyze.Core.Entities.PisteAggregate.Specifications;
using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class GetPistes : EndpointBaseAsync
    .WithRequest<GetPistesRequest>
    .WithActionResult<List<PisteDto>>
{
    private readonly IReadRepository<SkiArea> _skiAreaRepository;
    private readonly IReadRepository<Piste> _pisteRepository;
    private readonly IMapper _mapper;

    public GetPistes(IReadRepository<SkiArea> skiAreaRepository,
        IReadRepository<Piste> pisteRepository,
        IMapper mapper)
    {
        _skiAreaRepository = skiAreaRepository;
        _pisteRepository = pisteRepository;
        _mapper = mapper;
    }


    [HttpGet("/api/skiareas/{skiAreaId}/pistes")]
    public override async Task<ActionResult<List<PisteDto>>> HandleAsync([FromRoute] GetPistesRequest request, CancellationToken cancellationToken = default)
    {
        var skiArea = await _skiAreaRepository.FirstOrDefaultAsync(new GetSkiAreaWithNodesSpec(request.SkiAreaId));
        if (skiArea == null)
            return NotFound();

        var bounds = skiArea.Nodes.GetBounds();
        var pistes = await _pisteRepository.ListAsync(new PistesInBoundsSpec(bounds));

        var results = new List<Piste>();
        var polygon = skiArea.Nodes.ToList<ICoordinate>();
        // check if the piste ondolas start and endpoint is in bounds
        foreach (var piste in pistes)
        {
            var start = piste.Coordinates.First();
            var end = piste.Coordinates.Last();

            if (!polygon.ContainsPoint(start) || !polygon.ContainsPoint(end))
                continue;
            results.Add(piste);
        }

        var dtos = _mapper.Map<List<PisteDto>>(results);
        return Ok(dtos);
    }
}
