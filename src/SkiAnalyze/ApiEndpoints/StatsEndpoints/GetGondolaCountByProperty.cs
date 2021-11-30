using Swashbuckle.AspNetCore.Annotations;

namespace SkiAnalyze.ApiEndpoints.StatsEndpoints;

public class GetGondolaCountByProperty : BaseAsyncEndpoint
    .WithRequest<GetGondolaCountByPropertyRequest>
    .WithResponse<List<BaseStatValueDto<string, int>>>
{
    private readonly IStatsService _statsService;
    private readonly IMapper _mapper;

    public GetGondolaCountByProperty(IStatsService statsService,
        IMapper mapper)
    {
        _statsService = statsService;
        _mapper = mapper;
    }

    [HttpGet("/api/tracks/{trackId}/stats/gondolacount")]
    [SwaggerOperation(
        Summary = "Get the three top used gondola types",
        OperationId = "Stats.GondolaTypes",
        Tags = new[] { "StatsEndpoints" })
    ]
    public override async Task<ActionResult<List<BaseStatValueDto<string, int>>>> HandleAsync([FromRoute] GetGondolaCountByPropertyRequest request, CancellationToken cancellationToken = default)
    {
        var stats = await _statsService.GetGondolaCountByProperty(request.TrackId, request.PropertyName);
        var dtos = _mapper.Map<List<BaseStatValueDto<string, int>>>(stats);

        return Ok(dtos);
    }
}
