using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.PisteAggregate.Specifications;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Core.Services;

public class PisteSearchService : IPisteSearchService
{
    private const int MaxDiff = 2;

    private readonly IReadRepository<Piste> _pisteRepository;
    public PisteSearchService(IReadRepository<Piste> pisteRepository)
    {
        _pisteRepository = pisteRepository;
    }

    public async Task<List<Piste>> GetPistesInBounds(Bounds bounds)
    {
        ValidateCoordinates(bounds);
        
        var spec = new PistesInBoundsSpec(bounds);
        var results = await _pisteRepository.ListAsync(spec);
        return Result<List<Piste>>.Success(results);
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
