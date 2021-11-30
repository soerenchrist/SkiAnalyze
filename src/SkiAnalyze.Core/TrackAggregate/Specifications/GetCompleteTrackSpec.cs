using Ardalis.Specification;

namespace SkiAnalyze.Core.TrackAggregate.Specifications;

public class GetCompleteTrackSpec : Specification<Track>, ISingleResultSpecification
{
    public GetCompleteTrackSpec(int id)
    {
        Query.Where(x => x.Id == id)
            .AsNoTracking()
            .Include(x => x.AnalysisStatus)
            .AsNoTracking()
            .Include(x => x.Runs)
            .ThenInclude(x => x.Coordinates)
            .AsNoTracking();
    }
}
