using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.GondolaAggregate;

public class GondolaNode : BaseEntity<long>
{
    public long OsmId { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public long GondolaId { get; set; }
    public Gondola? Gondola { get; set; }
}
