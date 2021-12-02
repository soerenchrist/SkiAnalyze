using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.SkiAreaAggregate;

public class SkiAreaNode : BaseEntity<long>, ICoordinate
{
    public long OsmId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public long SkiAreaId { get; set; }
    public SkiArea? SkiArea { get; set; }
}
