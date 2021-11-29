using Ardalis.Specification;

namespace SkiAnalyze.Core.Common.Analysis.Specifications;

public class AnalysisBySessionAndId : Specification<AnalysisStatus>, ISingleResultSpecification
{
    public AnalysisBySessionAndId(Guid sessionId, Guid analysisId)
    {
        Query.Where(x => x.UserSessionId == sessionId)
            .Where(x => x.Id == analysisId);
    }
}
