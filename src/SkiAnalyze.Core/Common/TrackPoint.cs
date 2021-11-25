namespace SkiAnalyze.Core.Common;

public struct TrackPoint
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public DateTime DateTime { get; set; }
    public double? Elevation { get; set; }
}
