using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.PisteAggregate;

public class PisteNode : BaseEntity<long>
{
    public long OsmId { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public long PisteId { get; set; }
    public Piste? Piste { get; set; }
}
