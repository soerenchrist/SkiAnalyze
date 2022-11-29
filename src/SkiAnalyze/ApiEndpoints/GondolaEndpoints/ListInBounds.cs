namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class ListInBounds : EndpointBaseAsync
    .WithRequest<ListGondolasInBoundsRequest>
    .WithActionResult<ListGondolasInBoundsResponse>
{
    private readonly IGondolaSearchService _gondolaSearchService;
    private readonly IMapper _mapper;

    public ListInBounds(IGondolaSearchService gondolaSearchService,
        IMapper mapper)
    {

        _gondolaSearchService = gondolaSearchService;
        _mapper = mapper;
    }

    [HttpGet("/api/gondolas")]
    public override async Task<ActionResult<ListGondolasInBoundsResponse>> HandleAsync([FromQuery] ListGondolasInBoundsRequest request, 
        CancellationToken cancellationToken = default)
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
        var gondolas = await _gondolaSearchService.GetGondolasInBounds(new Bounds
        {
            SouthWest = sw,
            NorthEast = ne
        });

        var dtos = _mapper.Map<List<GondolaDto>>(gondolas);
        return new ListGondolasInBoundsResponse(dtos);
    }
}
