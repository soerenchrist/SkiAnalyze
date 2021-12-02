using Ardalis.Result;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.PisteAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IPisteSearchService
{
    Task<List<Piste>> GetPistesInBounds(Bounds bounds);
}
