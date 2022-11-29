namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetHeartRatePerPisteDifficulty : EndpointBaseAsync
    .WithRequest<GetHeartRatePerPisteDifficultyRequest>
    .WithActionResult<List<BaseStatValueDto<PisteDifficulty, double>>>
{
    private readonly IMapper _mapper;
    private readonly IStatsService _statsService;

    public GetHeartRatePerPisteDifficulty(IMapper mapper,
        IStatsService statsService)
    {
        _mapper = mapper;
        _statsService = statsService;
    }

    [HttpGet("/api/tracks/{trackId}/stats/heartRatesPerDiff")]
    public override async Task<ActionResult<List<BaseStatValueDto<PisteDifficulty, double>>>> HandleAsync([FromRoute] GetHeartRatePerPisteDifficultyRequest request, CancellationToken cancellationToken = default)
    {
        var stats = await _statsService.GetAverageHeartRatePerPisteDifficulty(request.TrackId);
        var dtos = _mapper.Map<List<BaseStatValueDto<PisteDifficulty, double>>>(stats);
        return dtos;
    }
}
