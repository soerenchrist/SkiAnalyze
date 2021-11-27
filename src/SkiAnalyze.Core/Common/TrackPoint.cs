using SkiAnalyze.Core.Interfaces.Common;

namespace SkiAnalyze.Core.Common;

public struct TrackPoint : ICoordinate
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime DateTime { get; set; }
    public double? Elevation { get; set; }
}
