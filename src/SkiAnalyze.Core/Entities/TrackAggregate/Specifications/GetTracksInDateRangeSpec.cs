using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.TrackAggregate.Specifications;

public class GetTracksInDateRangeSpec : Specification<Track>
{
    public GetTracksInDateRangeSpec(DateTime from, DateTime to)
    {
        Query.Where(x => x.Date >= from && x.Date < to); 
    }
}
