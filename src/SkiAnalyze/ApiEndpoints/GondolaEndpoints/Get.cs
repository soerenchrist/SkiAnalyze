using SkiAnalyze.Core.Entities.GondolaAggregate.Specifications;

namespace SkiAnalyze.ApiEndpoints.GondolaEndpoints;

public class Get : BaseAsyncEndpoint
    .WithRequest<GetGondolaRequest>
    .WithResponse<GondolaDto>
{
    private readonly IMapper _mapper;
    private readonly IReadRepository<Gondola> _gondolaRepository;

    public Get(IMapper mapper,
        IReadRepository<Gondola> gondolaRepository)
    {
        _mapper = mapper;
        _gondolaRepository = gondolaRepository;
    }

    [HttpGet("/api/gondolas/{id}")]
    public override async Task<ActionResult<GondolaDto>> HandleAsync([FromRoute] GetGondolaRequest request, CancellationToken cancellationToken = default)
    {
        var gondola = await _gondolaRepository.GetBySpecAsync(new GondolaByIdSpec(request.Id));
        if (gondola == null)
            return NotFound();

        var dto = _mapper.Map<GondolaDto>(gondola);
        return dto;
    }
}
