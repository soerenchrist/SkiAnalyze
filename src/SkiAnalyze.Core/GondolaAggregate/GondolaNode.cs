using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.GondolaAggregate;

public class GondolaNode : BaseEntity<long>
{
    public long OsmId { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public long GondolaId { get; set; }
    public Gondola? Gondola { get; set; }
}
