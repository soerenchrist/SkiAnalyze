using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Create : BaseAsyncEndpoint
    .WithRequest<CreateTrackRequest>
    .WithResponse<TrackDto>
{
    private readonly IRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;

    public Create(IRepository<Track> tracksRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _tracksRepository = tracksRepository;
    }

    [HttpPost("/api/tracks")]
    [SwaggerOperation(
        Summary = "Create a new track",
        Description = "Create a new track",
        OperationId = "Tracks.Create",
        Tags = new[] { "TrackEndpoints" })
    ]
    public override async Task<ActionResult<TrackDto>> HandleAsync([FromForm] CreateTrackRequest request, CancellationToken cancellationToken = default)
    {
        using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream);

        var bytes = memoryStream.ToArray();

        var track = new Track
        {
            Name = request.Name,
            FileContents = bytes,
            FileType = request.FileType,
        };

        var result = await _tracksRepository.AddAsync(track, cancellationToken);
        var dto = _mapper.Map<TrackDto>(result);
        return dto;
    }
}
