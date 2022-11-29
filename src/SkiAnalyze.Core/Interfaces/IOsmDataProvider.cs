
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IOsmDataProvider : IDisposable
{
    Task<IEnumerable<SkiArea>> GetSkiAreas(Coordinate northEast, Coordinate southWest);
    Task<IEnumerable<Gondola>> GetGondolas(Coordinate northEast, Coordinate southWest);
    Task<IEnumerable<Piste>> GetPistes(Coordinate northEast, Coordinate southWest);
}
