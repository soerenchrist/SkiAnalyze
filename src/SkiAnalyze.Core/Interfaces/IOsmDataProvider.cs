
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IOsmDataProvider
{
    Task<IEnumerable<SkiArea>> GetSkiAreas(Coordinate northWest, Coordinate southEast);
    Task<IEnumerable<Gondola>> GetGondolas(Coordinate northWest, Coordinate southEast);
    Task<IEnumerable<Piste>> GetPistes(Coordinate northWest, Coordinate southEast);
}
