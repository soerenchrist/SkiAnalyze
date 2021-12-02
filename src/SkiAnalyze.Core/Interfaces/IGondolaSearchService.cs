using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IGondolaSearchService
{
    Task<List<Gondola>> GetGondolasInBounds(Bounds bounds);
}
