namespace SkiAnalyze.ApiEndpoints.StatsEndpoints.Totals;

public class GetTotals : BaseAsyncEndpoint
    .WithoutRequest
    .WithResponse<TotalsDto>
{
    private readonly IReadRepository<Track> _tracksRepository;
    private readonly IReadRepository<Run> _runRepository;

    public GetTotals(IReadRepository<Track> tracksRepository,
        IReadRepository<Run> runRepository)
    {
        _tracksRepository = tracksRepository;
        _runRepository = runRepository;
    }

    [HttpGet("/api/stats/totals")]
    public override async Task<ActionResult<TotalsDto>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var trackCount = await _tracksRepository.CountAsync(cancellationToken);
        var runs = await _runRepository.ListAsync(cancellationToken);

        var maxSpeed = 0.0;
        try
        {
            maxSpeed = runs.Max(x => x.MaxSpeed);
        } catch(InvalidOperationException) { }

        return new TotalsDto
        {
            TotalDistance = runs.Sum(x => x.TotalDistance),
            TotalElevation = runs.Sum(x => x.TotalElevation),
            TotalRuns = runs.Where(x => x.Downhill).Count(),
            TotalGondolas = runs.Where(x => !x.Downhill).Count(),
            TotalTracks = trackCount,
            MaxSpeed = maxSpeed,
            TotalCalories = runs.Sum(x => x.TotalCalories) ?? 0
        };
    }
}
