using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.GondolaAggregate.Specifications;
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

    public async Task<Result<List<Gondola>>> GetGondolasInBounds(Coordinate southWest, Coordinate northEast)
    {
        var result = ValidateCoordinates(southWest, northEast);
        if (!result.IsSuccess)
            return result;

        var spec = new GondolasInBoundsSpec(southWest, northEast);
        var results = await _gondolaRepository.ListAsync(spec);
        return Result<List<Gondola>>.Success(results);
    }

    private Result<List<Gondola>> ValidateCoordinates(Coordinate southWest, Coordinate northEast)
    {
        if (southWest.Longitude > northEast.Longitude)
            return Result<List<Gondola>>.Invalid(new List<ValidationError>() 
            { 
                new ValidationError
                {
                    ErrorMessage = "NorthWest Longitude must not be greater than southEast Longitude"
                } 
            });
        if (southWest.Latitude > northEast.Latitude)
            return Result<List<Gondola>>.Invalid(new List<ValidationError>() { 
                new ValidationError
                {
                    ErrorMessage = "NorthWest Latitude must not be smaller than southEast Latitude"
                } 
            });

        var latDiff = northEast.Latitude - southWest.Latitude;
        var lonDiff = northEast.Longitude - southWest.Longitude;

        if (latDiff > MaxDiff || lonDiff > MaxDiff)
        {
            return Result<List<Gondola>>.Invalid(new List<ValidationError>() { 
                new ValidationError
                {
                    ErrorMessage = $"Range between Latitude and Longitude must not exceed {MaxDiff}"
                } 
            });
        }

        return Result<List<Gondola>>.Success(new List<Gondola>());;
    }
}
