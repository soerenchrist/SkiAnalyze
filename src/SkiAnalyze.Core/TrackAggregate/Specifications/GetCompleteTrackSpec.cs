using Ardalis.Specification;

namespace SkiAnalyze.Core.TrackAggregate.Specifications;

public class GetCompleteTrackSpec : Specification<Track>, ISingleResultSpecification
{
    public GetCompleteTrackSpec(int id)
    {
        Query.Where(x => x.Id == id)
            .Include(x => x.AnalysisStatus)
            .Include(x => x.Runs!)
            .ThenInclude(x => x.Gondola)
            .Include(x => x.Runs!)
            .ThenInclude(x => x.Coordinates)
            .ThenInclude(x => x.Piste);
    }
}
