using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.TrackAggregate.Specifications;

public class GetTracksWithSkiAreaSpec : Specification<Track>
{
    public GetTracksWithSkiAreaSpec()
    {
        Query.Include(x => x.SkiArea)
            .OrderByDescending(x => x.Date);
    }
}
