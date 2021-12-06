using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.TrackAggregate.Specifications;

public class GetTrackWithSkiAreaSpec : Specification<Track>, ISingleResultSpecification
{
    public GetTrackWithSkiAreaSpec(int id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.SkiArea);
    }
}
