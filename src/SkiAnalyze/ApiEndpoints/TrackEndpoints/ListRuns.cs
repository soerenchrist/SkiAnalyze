namespace SkiAnalyze.ApiEndpoints.TrackEndpoints;

public class ListRuns : BaseAsyncEndpoint
    .WithRequest<ListRunsRequest>
    .WithResponse<List<RunDto>>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;

    public ListRuns(IReadRepository<Track> tracksRepository,
        IMapper mapper)
    {
        _tracksRepository = tracksRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks/{trackId}/runs")]
    public override async Task<ActionResult<List<RunDto>>> HandleAsync([FromRoute] ListRunsRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.GetBySpecAsync(new GetTrackWithRunsSpec(request.TrackId), cancellationToken);
        if (track == null)
            return NotFound();

        var dtos = _mapper.Map<List<RunDto>>(track.Runs);
        return dtos;
    }
}
