using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Get : BaseAsyncEndpoint
    .WithoutRequest
    .WithResponse<List<TrackDto>>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;

    public Get(IReadRepository<Track> tracksRepository,
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
        var tracks = await _tracksRepository.ListAsync(cancellationToken);
        var dtos = _mapper.Map<List<TrackDto>>(tracks);

        return dtos;
    }
}
