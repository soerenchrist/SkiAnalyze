using Ardalis.Specification;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

public class GetSkiAreasInBoundsSpec : Specification<SkiArea>
{
    public GetSkiAreasInBoundsSpec(Bounds bounds)
    {
        var southWest = bounds.SouthWest;
        var northEast = bounds.NorthEast;

        southWest.Latitude -= 0.1;
        southWest.Longitude -= 0.1;

        northEast.Latitude += 0.1;
        northEast.Longitude += 0.1;

        Query
            .Include(x => x.Nodes)
            .Where(x => x.Nodes.Min(x => x.Longitude) > southWest.Longitude
            && x.Nodes.Min(x => x.Latitude) > southWest.Latitude
            && x.Nodes.Max(x => x.Longitude) < northEast.Longitude
            && x.Nodes.Max(x => x.Latitude) < northEast.Latitude);
    }
}
