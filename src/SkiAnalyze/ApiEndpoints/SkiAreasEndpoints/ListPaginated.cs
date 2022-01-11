using SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.SkiAreasEndpoints;

public class ListPaginated : BaseAsyncEndpoint
    .WithRequest<ListPaginatedRequest>
    .WithResponse<PaginatedList<SkiAreaDto>>
{
    private readonly IReadRepository<SkiArea> _areaRepository;
    private readonly IMapper _mapper;

    public ListPaginated(IReadRepository<SkiArea> areaRepository,
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
    public override async Task<ActionResult<PaginatedList<SkiAreaDto>>> HandleAsync([FromQuery] ListPaginatedRequest request, CancellationToken cancellationToken = default)
    {
        Bounds? bounds = null;
        if (request.SwLat.HasValue
            && request.SwLon.HasValue
            && request.NeLat.HasValue
            && request.NeLon.HasValue)
        {
            var sw = new Coordinate
            {
                Latitude = request.SwLat.Value,
                Longitude = request.SwLon.Value
            };
            var ne = new Coordinate
            {
                Latitude = request.NeLat.Value,
                Longitude = request.NeLon.Value
            };
            bounds = new Bounds
            {
                NorthEast = ne,
                SouthWest = sw,
            };
        }

        var spec = new GetSkiAreasPaginatedSpec(
            bounds: bounds,
            searchText: request.SearchText,
            page: request.Page,
            pageSize: request.PageSize);
        var countSpec = new GetSkiAreasPaginatedSpec(
            bounds: bounds,
            searchText: request.SearchText);
        var skiAreas = await _areaRepository.ListAsync(spec, cancellationToken);
        var totalCount = await _areaRepository.CountAsync(countSpec, cancellationToken);
        var dtos = _mapper.Map<List<SkiAreaDto>>(skiAreas);

        return new PaginatedList<SkiAreaDto>(dtos, totalCount, request.Page);
    }
}
