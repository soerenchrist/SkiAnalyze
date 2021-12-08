namespace SkiAnalyze.ApiEndpoints.StatsEndpoints.Totals;

public class GetTimeline : BaseAsyncEndpoint
    .WithRequest<GetTimelineRequest>
    .WithResponse<List<BaseStatValueDto<DateTime, double>>>
{
    private readonly IReadRepository<Track> _tracksRepository;

    public GetTimeline(IReadRepository<Track> tracksRepository)
    {
        _tracksRepository = tracksRepository;
    }

    [HttpGet("/api/stats/timeline")]
    public override async Task<ActionResult<List<BaseStatValueDto<DateTime, double>>>> HandleAsync([FromQuery] GetTimelineRequest request, CancellationToken cancellationToken = default)
    {
        var today = DateTime.Today;
        var (startDate, endDate, stepInDays) = (request.DateRange, request.Year) switch
        {
            ("week", _) => (today.AddDays(-7), today, 1),
            ("month", _) => (today.AddDays(-90), today, 1),
            ("3month", _) => (today.AddDays(-90), today, 7),
            ("6month", _) => (today.AddDays(-120), today, 7),
            (_, int year) when year < DateTime.Now.Year => (new DateTime(year, 1, 1), new DateTime(year, 12, 31), 30), 
            _ => (today.AddDays(-365), today, 30) // "year"
        };

        Func<Track, double> selector = request.ByProperty switch
        {
            "elevation" => (Track track) => track.TotalElevation,
            "calories" => (Track track) => track.TotalCalories ?? 0,
            _ => (Track track) => track.TotalDistance
        };

        var results = new List<BaseStatValueDto<DateTime, double>>();
        var startOfRange = startDate;
        while(startOfRange < endDate)
        {
            var endOfRange = startOfRange.AddDays(stepInDays);
            
            var tracks = await _tracksRepository.ListAsync(new GetTracksInDateRangeSpec(startOfRange, endOfRange), cancellationToken);
            var value = tracks.Sum(selector);
            results.Add(new BaseStatValueDto<DateTime, double>(startOfRange, value));

            startOfRange = endOfRange.AddDays(1);
        }

        return results;
    }
}
