using Ardalis.Specification;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Entities.GondolaAggregate.Specifications;

public sealed class GondolasInBoundsSpec : Specification<Gondola>
{
    public GondolasInBoundsSpec(Bounds bounds)
    {
        var southWest = bounds.SouthWest;
        var northEast = bounds.NorthEast;
        Query
            .Include(x => x.Coordinates)
            .Include(x => x.Runs)
            .Where(x => x.Coordinates.Min(x => x.Longitude) > southWest.Longitude
            && x.Coordinates.Min(x => x.Latitude) > southWest.Latitude
            && x.Coordinates.Max(x => x.Longitude) < northEast.Longitude
            && x.Coordinates.Max(x => x.Latitude) < northEast.Latitude);
    }
}
