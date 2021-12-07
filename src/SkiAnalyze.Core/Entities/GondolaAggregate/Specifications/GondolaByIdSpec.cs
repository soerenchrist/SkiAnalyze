using Ardalis.Specification;

namespace SkiAnalyze.Core.Entities.GondolaAggregate.Specifications;

public class GondolaByIdSpec : Specification<Gondola>, ISingleResultSpecification
{
    public GondolaByIdSpec(long gondolaId)
    {
        Query.Where(x => x.Id == gondolaId)
            .Include(x => x.Coordinates);
    }
}
