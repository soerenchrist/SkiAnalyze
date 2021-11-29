using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Common;

public class TrackPoint : BaseEntity<int>, ICoordinate
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime DateTime { get; set; }
    public double? Elevation { get; set; }
    public Piste? Piste { get; set; }
    public long? PisteId { get; set; }
    public Run? Run { get; set; }
    public int RunId { get; set; }
}
