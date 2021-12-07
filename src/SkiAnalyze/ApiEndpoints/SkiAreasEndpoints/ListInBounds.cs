using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class ListInBounds : BaseAsyncEndpoint
    .WithRequest<ListInBoundsRequest>
    .WithResponse<List<SkiAreaDto>>
{
    private readonly IReadRepository<SkiArea> _areaRepository;
    private readonly IMapper _mapper;

    public ListInBounds(IReadRepository<SkiArea> areaRepository,
        IMapper mapper)
    {
        _areaRepository = areaRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/skiareas")]
    [SwaggerOperation(
        Summary = "Gets a list of all ski areas in bounds",
        Description = "Gets a list of all ski areas in a specific bound",
        OperationId = "SkiAreas.ListInBounds",
        Tags = new[] { "SkiAreasEndpoints" })
    ]
    public override async Task<ActionResult<List<SkiAreaDto>>> HandleAsync([FromQuery] ListInBoundsRequest request, CancellationToken cancellationToken = default)
    {
        var sw = new Coordinate
        {
            Latitude = request.SwLat,
            Longitude = request.SwLon
        };
        var ne = new Coordinate
        {
            Latitude = request.NeLat,
            Longitude = request.NeLon
        };
        var bounds = new Bounds
        {
            NorthEast = ne,
            SouthWest = sw,
        };

        var skiAreas = await _areaRepository.ListAsync(new GetSkiAreasInBoundsSpec(bounds));
        var dtos = _mapper.Map<List<SkiAreaDto>>(skiAreas);
        return dtos;
    }
}
