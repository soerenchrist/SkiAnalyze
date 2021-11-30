using Ardalis.Specification;

namespace SkiAnalyze.Core.TrackAggregate.Specifications;

public class GetTrackWithStatusSpec : Specification<Track>, ISingleResultSpecification
{
    public GetTrackWithStatusSpec(int id)
    {
        Query.Where(x => x.Id == id)
            .Include(x => x.AnalysisStatus);
    }
}
