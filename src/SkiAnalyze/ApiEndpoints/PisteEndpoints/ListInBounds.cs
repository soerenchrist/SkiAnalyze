namespace SkiAnalyze.ApiEndpoints.PisteEndpoints;

public class ListInBounds : EndpointBaseAsync
    .WithRequest<ListPistesInBoundsRequest>
    .WithActionResult<ListPistesInBoundsResponse>
{
    private readonly IPisteSearchService _pisteSearchService;
    private readonly IMapper _mapper;

    public ListInBounds(IPisteSearchService pisteSearchService,
        IMapper mapper)
    {
        _pisteSearchService = pisteSearchService;
        _mapper = mapper;
    }

    [HttpGet("/api/pistes")]
    public override async Task<ActionResult<ListPistesInBoundsResponse>> HandleAsync([FromQuery] ListPistesInBoundsRequest request, CancellationToken cancellationToken = default)
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
        var pistes = await _pisteSearchService.GetPistesInBounds(new Bounds
        {
            SouthWest = sw,
            NorthEast = ne
        });

        var dtos = _mapper.Map<List<PisteDto>>(pistes);
        return new ListPistesInBoundsResponse(dtos);
    }
}