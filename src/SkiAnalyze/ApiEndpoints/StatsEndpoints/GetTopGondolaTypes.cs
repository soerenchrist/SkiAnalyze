using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetTopGondolaTypes : BaseAsyncEndpoint
    .WithRequest<GetTopGondolaTypesRequest>
    .WithResponse<List<BaseStatValueDto<string, int>>>
{
    private readonly IStatsService _statsService;
    private readonly IMapper _mapper;

    public GetTopGondolaTypes(IStatsService statsService,
        IMapper mapper)
    {
        _statsService = statsService;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks/{trackId}/stats/gondolatypes")]
    [SwaggerOperation(
        Summary = "Get the three top used gondola types",
        OperationId = "Stats.GondolaTypes",
        Tags = new[] { "StatsEndpoints" })
    ]
    public override async Task<ActionResult<List<BaseStatValueDto<string, int>>>> HandleAsync([FromRoute] GetTopGondolaTypesRequest request, CancellationToken cancellationToken = default)
    {
        var stats = await _statsService.GetTopGondolaTypes(request.TrackId);
        var dtos = _mapper.Map<List<BaseStatValueDto<string, int>>>(stats);

        return Ok(dtos);
    }
}
