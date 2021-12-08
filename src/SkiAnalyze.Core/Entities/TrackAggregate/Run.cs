using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.TrackAggregate;

public class Run : BaseEntity<int>
{
    public int Number { get; set; }
    public bool Downhill { get; set; }
    public Gondola? Gondola { get; set; }
    public long? GondolaId { get; set; }
    public List<TrackPoint> Coordinates { get; set; } = new();
    public int TrackId { get; set; }
    public Track? Track { get; set; }

    public double? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public int? TotalCalories { get; set; }
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double AverageSpeed { get; set; }
    public double MaxSpeed { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}