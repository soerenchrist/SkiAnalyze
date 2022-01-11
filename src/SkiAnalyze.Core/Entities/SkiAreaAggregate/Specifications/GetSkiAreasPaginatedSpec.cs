using Ardalis.Specification;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Entities.SkiAreaAggregate.Specifications;

public sealed class GetSkiAreasPaginatedSpec : Specification<SkiArea>
{
    public GetSkiAreasPaginatedSpec(Bounds? bounds = null, string? searchText = null,
        int? page = null, int? pageSize = null)
    {
        Query.OrderBy(x => x.Name);
        if (bounds != null)
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

        if (searchText != null)
        {
            Query.Where(x => x.Name.ToLower().Contains(searchText.ToLower()));
        }

        if (page.HasValue && pageSize.HasValue)
        {
            Query.Skip((page.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }
    }
}
