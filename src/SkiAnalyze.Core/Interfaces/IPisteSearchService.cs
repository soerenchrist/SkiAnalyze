using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IPisteSearchService
{
    Task<Result<List<Piste>>> GetPistesInBounds(Coordinate southWest, Coordinate northEast);
}
