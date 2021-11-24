
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Interfaces;

public interface IOsmDataProvider
{
    Task<IEnumerable<Gondola>> GetGondolas(Coordinate northWest, Coordinate southEast);
    Task<IEnumerable<Piste>> GetPistes(Coordinate northWest, Coordinate southEast);
}
