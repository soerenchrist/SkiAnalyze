using Ardalis.Specification;

namespace SkiAnalyze.Core.SessionAggregate.Specifications;

public class GetCompleteSession : Specification<UserSession>, ISingleResultSpecification
{
    public GetCompleteSession(Guid userSessionId)
    {
        Query.Where(x => x.Id == userSessionId)
            .Include(x => x.Tracks)
                .ThenInclude(x => x.Runs!)
                .ThenInclude(x => x.Gondola)
            .Include(x => x.Tracks)
                .ThenInclude(x => x.Runs!)
                .ThenInclude(x => x.Coordinates);
    }
}
