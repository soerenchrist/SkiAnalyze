using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetTopGondolas : EndpointBaseAsync
    .WithRequest<GetTopGondolasRequest>
    .WithActionResult<List<BaseStatValueDto<Gondola, int>>>
{
    private readonly IStatsService _statsService;
    private readonly IMapper _mapper;

    public GetTopGondolas(IStatsService statsService,
        IMapper mapper)
    {
        _statsService = statsService;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks/{trackId}/stats/gondolas")]
    [SwaggerOperation(
        Summary = "Get the three top used gondolas",
        OperationId = "Stats.Gondolas",
        Tags = new[] { "StatsEndpoints" })
    ]
    public override async Task<ActionResult<List<BaseStatValueDto<Gondola, int>>>> HandleAsync([FromRoute] GetTopGondolasRequest request, CancellationToken cancellationToken = default)
    {
        var stats = await _statsService.GetTopGondolas(request.TrackId);
        var dtos = _mapper.Map<List<BaseStatValueDto<GondolaDto, int>>>(stats);

        return Ok(dtos);
    }
}
