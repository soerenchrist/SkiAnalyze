using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.PisteAggregate;

public class PisteNode : BaseEntity<long>, ICoordinate
{
    public long OsmId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public long PisteId { get; set; }
    public Piste? Piste { get; set; }
}
