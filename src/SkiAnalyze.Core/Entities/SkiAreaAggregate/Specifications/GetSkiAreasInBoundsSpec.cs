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
            .Where(x => x.CenterLatitude > southWest.Latitude
            && x.CenterLongitude > southWest.Longitude
            && x.CenterLatitude < northEast.Latitude
            && x.CenterLongitude < northEast.Longitude);
    }
}
