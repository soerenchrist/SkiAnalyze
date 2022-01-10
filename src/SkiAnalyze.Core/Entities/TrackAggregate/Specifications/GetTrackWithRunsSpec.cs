using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.TrackAggregate.Specifications;

public sealed class GetTrackWithRunsSpec : Specification<Track>, ISingleResultSpecification
{
    public GetTrackWithRunsSpec(int trackId)
    {
        Query.Where(x => x.Id == trackId)
            .Include(x => x.Runs)
            .ThenInclude(x => x.Gondola);
    }
}
