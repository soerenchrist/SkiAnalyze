using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

public class GetSkiAreaWithNodesSpec : Specification<SkiArea>, ISingleResultSpecification
{
    public GetSkiAreaWithNodesSpec(long areaId)
    {
        Query.Where(x => x.Id == areaId)
            .Include(x => x.Nodes);
    }
}
