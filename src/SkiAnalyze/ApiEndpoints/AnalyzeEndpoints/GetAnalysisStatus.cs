namespace SkiAnalyze.ApiEndpoints.AnalyzeEndpoints;

public class GetAnalysisStatus : EndpointBaseAsync
    .WithRequest<GetAnalysisStatusRequest>
    .WithActionResult<AnalysisStatusDto>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IMapper _mapper;
    public GetAnalysisStatus(IReadRepository<Track> tracksRepository,
        IMapper mapper)
        => (_tracksRepository, _mapper) = (tracksRepository, mapper);

    [HttpGet("/api/tracks/{trackId:int}/analysis/status")]
    public override async Task<ActionResult<AnalysisStatusDto>> HandleAsync([FromRoute] GetAnalysisStatusRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _tracksRepository.FirstOrDefaultAsync(new GetTrackWithStatusSpec(request.TrackId));
        if (track?.AnalysisStatus == null)
            return NotFound();

        var dto = _mapper.Map<AnalysisStatusDto>(track.AnalysisStatus);
        return Ok(dto);
    }
}
