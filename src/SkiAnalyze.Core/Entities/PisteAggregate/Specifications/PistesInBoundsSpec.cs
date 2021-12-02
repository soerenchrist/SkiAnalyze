using Ardalis.Specification;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Entities.PisteAggregate.Specifications;

public class PistesInBoundsSpec : Specification<Piste>
{
    public PistesInBoundsSpec(Bounds bounds)
    {
        var southWest = bounds.SouthWest;
        var northEast = bounds.NorthEast;
        Query
            .Include(x => x.Coordinates)
            .Where(x => x.Coordinates.Min(x => x.Longitude) > southWest.Longitude
            && x.Coordinates.Min(x => x.Latitude) > southWest.Latitude
            && x.Coordinates.Max(x => x.Longitude) < northEast.Longitude
            && x.Coordinates.Max(x => x.Latitude) < northEast.Latitude);
    }
}
