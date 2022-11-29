using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.GondolaAggregate.Specifications;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Core.Services;

public class GondolaSearchService : IGondolaSearchService
{
    private const int MaxDiff = 2;

    private readonly IReadRepository<Gondola> _gondolaRepository;
    public GondolaSearchService(IReadRepository<Gondola> gondolaRepository)
    {
        _gondolaRepository = gondolaRepository;
    }

    public async Task<List<Gondola>> GetGondolasInBounds(Bounds bounds)
    {
        ValidateCoordinates(bounds);
        
        var spec = new GondolasInBoundsSpec(bounds);
        var results = await _gondolaRepository.ListAsync(spec);
        return Result<List<Gondola>>.Success(results);
    }

    private void ValidateCoordinates(Bounds bounds)
    {
        var southWest = bounds.SouthWest;
        var northEast = bounds.NorthEast;
        if (southWest.Longitude > northEast.Longitude)
            throw new ArgumentException("NorthWest Longitude must not be greater than southEast Longitude");
        if (southWest.Latitude > northEast.Latitude)
            throw new ArgumentException("NorthWest Latitude must not be smaller than southEast Latitude");

        var latDiff = northEast.Latitude - southWest.Latitude;
        var lonDiff = northEast.Longitude - southWest.Longitude;

        if (latDiff > MaxDiff || lonDiff > MaxDiff)
        {
            throw new ArgumentException($"Range between Latitude and Longitude must not exceed {MaxDiff}");
        }

    }
}
