using Ardalis.Specification;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.TrackAggregate.Specifications;

internal class GetTrackPointsForTrackSpec : Specification<TrackPoint>
{
    public GetTrackPointsForTrackSpec(int trackId)
    {
        Query
            .Include(x => x.Run)
            .Where(x => x.Run!.TrackId == trackId)
            .Include(x => x.Piste);
    }
}
