using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetDifficultyStats : BaseAsyncEndpoint
    .WithRequest<GetDifficultyStatsRequest>
    .WithResponse<List<BaseStatValueDto<PisteDifficulty, double>>>
{
    private readonly IStatsService _statsService;
    private readonly IMapper _mapper;

    public GetDifficultyStats(IStatsService statsService,
        IMapper mapper)
    {
        _statsService = statsService;
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

        var result = await _statsService.GetDifficultyStats(request.TrackId);
        var dtos = _mapper.Map<List<BaseStatValueDto<PisteDifficulty, double>>>(result);
        return Ok(dtos);
    }
}
