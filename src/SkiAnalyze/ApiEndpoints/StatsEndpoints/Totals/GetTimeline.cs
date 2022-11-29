namespace SkiAnalyze.ApiEndpoints.StatsEndpoints.Totals;

public class GetTimeline : EndpointBaseAsync
    .WithRequest<GetTimelineRequest>
    .WithActionResult<List<BaseStatValueDto<DateTime, double>>>
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
        DateTime startDate, endDate;

        if (request.EndDate == null)
        {
            endDate = today;
        } else
        {
            endDate = request.EndDate.Value;
        }

        if (request.StartDate == null)
        {
            startDate = endDate.AddDays(-365);
        } else
        {
            startDate = request.StartDate.Value;
        }

        var difference = (endDate - startDate).Days;

        var stepInDays = difference switch
        {
            < 30 => 1,
            < 50 => 7,
            _ => 30,
        };

        Func<Track, double> selector = request.ByProperty switch
        {
            "elevation" => (Track track) => track.TotalElevation,
            "calories" => (Track track) => track.TotalCalories ?? 0,
            _ => (Track track) => track.TotalDistance
        };

        var results = new List<BaseStatValueDto<DateTime, double>>();
        var startOfRange = startDate;
        while(startOfRange <= endDate)
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
