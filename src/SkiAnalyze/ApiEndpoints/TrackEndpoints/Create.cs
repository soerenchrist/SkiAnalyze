namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class Create : EndpointBaseAsync
    .WithRequest<CreateTrackRequest>
    .WithActionResult<TrackDto>
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
    public override async Task<ActionResult<TrackDto>> HandleAsync([FromForm] CreateTrackRequest request, CancellationToken cancellationToken = default)
    {
        using var memoryStream = new MemoryStream();
        await request.File.CopyToAsync(memoryStream);

        var bytes = memoryStream.ToArray();

        var track = new Track
        {
            FileContents = bytes,
            FileType = request.FileType,
        };

        var result = await _tracksRepository.AddAsync(track, cancellationToken);
        var dto = _mapper.Map<TrackDto>(result);
        return dto;
    }
}
