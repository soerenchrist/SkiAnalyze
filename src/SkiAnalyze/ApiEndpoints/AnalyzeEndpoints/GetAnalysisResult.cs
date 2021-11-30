
namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisResult : BaseAsyncEndpoint
    .WithRequest<GetAnalysisResultRequest>
    .WithResponse<AnalysisResultDto>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;

    public GetAnalysisResult(IReadRepository<Track> tracksRepository,
        IMapper mapper)
    {
        _mapper = mapper;
        _tracksRepository = tracksRepository;
    }

    [HttpGet("/api/tracks/{trackId:int}/analysis/result")]
    public override async Task<ActionResult<AnalysisResultDto>> HandleAsync([FromRoute] GetAnalysisResultRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.GetBySpecAsync(new GetCompleteTrackSpec(request.TrackId));
        if (track == null)
            return NotFound();

        var runDtos = _mapper.Map<List<RunDto>>(track.Runs);
        var dto = new AnalysisResultDto
        {
            Runs = runDtos,
            Bounds = runDtos.SelectMany(x => x.Coordinates)
                .Select(x => (ICoordinate)x)
                .GetBounds()
        };
        return Ok(dto);
    }
}
