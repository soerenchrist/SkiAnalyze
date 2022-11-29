using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<List<TrackDto>>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;

    public List(IReadRepository<Track> tracksRepository,
        IMapper mapper)
    {
        _tracksRepository = tracksRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks")]
    [SwaggerOperation(
        Summary = "Get tracks",
        Description = "Get all tracks",
        OperationId = "Tracks.Get",
        Tags = new[] { "TrackEndpoints" })
    ]
    public override async Task<ActionResult<List<TrackDto>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var tracks = await _tracksRepository.ListAsync(new GetTracksWithSkiAreaSpec(), cancellationToken);
        var dtos = _mapper.Map<List<TrackDto>>(tracks);

        return dtos;
    }
}
