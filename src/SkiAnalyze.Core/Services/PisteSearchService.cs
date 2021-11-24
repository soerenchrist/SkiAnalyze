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

    public async Task<Result<List<Piste>>> GetPistesInBounds(Coordinate southWest, Coordinate northEast)
    {
        var result = ValidateCoordinates(southWest, northEast);
        if (!result.IsSuccess)
            return result;

        var spec = new PistesInBoundsSpec(southWest, northEast);
        var results = await _pisteRepository.ListAsync(spec);
        return Result<List<Piste>>.Success(results);
    }

    private Result<List<Piste>> ValidateCoordinates(Coordinate southWest, Coordinate northEast)
    {
        if (southWest.Longitude > northEast.Longitude)
            return Result<List<Piste>>.Invalid(new List<ValidationError>() 
            { 
                new ValidationError
                {
                    ErrorMessage = "NorthWest Longitude must not be greater than southEast Longitude"
                } 
            });
        if (southWest.Latitude > northEast.Latitude)
            return Result<List<Piste>>.Invalid(new List<ValidationError>() { 
                new ValidationError
                {
                    ErrorMessage = "NorthWest Latitude must not be smaller than southEast Latitude"
                } 
            });

        var latDiff = northEast.Latitude - southWest.Latitude;
        var lonDiff = northEast.Longitude - southWest.Longitude;

        if (latDiff > MaxDiff || lonDiff > MaxDiff)
        {
            return Result<List<Piste>>.Invalid(new List<ValidationError>() { 
                new ValidationError
                {
                    ErrorMessage = $"Range between Latitude and Longitude must not exceed {MaxDiff}"
                } 
            });
        }

        return Result<List<Piste>>.Success(new List<Piste>());;
    }
}
