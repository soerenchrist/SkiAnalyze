namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class GetDetail : BaseAsyncEndpoint
    .WithRequest<GetDetailRequest>
    .WithResponse<TrackDto>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;

    public GetDetail(IReadRepository<Track> tracksRepository,
        IMapper mapper)
    {
        _tracksRepository = tracksRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks/{trackId}")]
    public override async Task<ActionResult<TrackDto>> HandleAsync([FromRoute] GetDetailRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.GetBySpecAsync(new GetTrackWithSkiAreaSpec(request.TrackId), cancellationToken);
        if (track == null)
            return NotFound();

        var dto = _mapper.Map<TrackDto>(track);
        return Ok(dto);
    }
}
