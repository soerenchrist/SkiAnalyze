using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.GondolaAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IGondolaSearchService
{
    Task<Result<List<Gondola>>> GetGondolasInBounds(Coordinate southWest, Coordinate northEast);
}
