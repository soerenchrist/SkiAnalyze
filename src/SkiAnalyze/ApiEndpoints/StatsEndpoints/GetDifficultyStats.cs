using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetDifficultyStats : EndpointBaseAsync
    .WithRequest<GetDifficultyStatsRequest>
    .WithActionResult<List<BaseStatValueDto<PisteDifficulty, double>>>
{
    private readonly IStatsService _statsService;
    private readonly IReadRepository<Track> _trackRepository;
    private readonly IMapper _mapper;

    public GetDifficultyStats(IStatsService statsService,
        IMapper mapper,
        IReadRepository<Track> trackRepository)
    {
        _statsService = statsService;
        _trackRepository = trackRepository;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks/{trackId}/stats/difficulty")]
    [SwaggerOperation(
        Summary = "Gets the percentage of points per piste difficulty",
        OperationId = "Stats.Difficulty",
        Tags = new[] { "StatsEndpoints" })
    ]
    public override async Task<ActionResult<List<BaseStatValueDto<PisteDifficulty, double>>>> HandleAsync([FromRoute] GetDifficultyStatsRequest request, CancellationToken cancellationToken = default)
    {
        var track = await _trackRepository.GetByIdAsync(request.TrackId);
        if (track == null)
            return NotFound();
        var result = await _statsService.GetDifficultyStats(request.TrackId);
        var dtos = _mapper.Map<List<BaseStatValueDto<PisteDifficulty, double>>>(result);
        return Ok(dtos);
    }
}
