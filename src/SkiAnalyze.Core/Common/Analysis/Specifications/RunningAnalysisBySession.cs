using Ardalis.Specification;

namespace SkiAnalyze.Core.Common.Analysis.Specifications;

public class RunningAnalysisBySession : Specification<AnalysisStatus>, ISingleResultSpecification
{
    public RunningAnalysisBySession(Guid session)
    {
        Query.Where(x => x.IsFinished == false)
            .Where(x => x.UserSessionId == session);
    }
}
