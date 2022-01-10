using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.TrackAggregate.Specifications;

public sealed class GetRunsForTrackSpec : Specification<Run>
{
    public GetRunsForTrackSpec(int trackId)
    {
        Query
            .Where(x => x.TrackId == trackId)
            .Include(x => x.Gondola);
    }
}
