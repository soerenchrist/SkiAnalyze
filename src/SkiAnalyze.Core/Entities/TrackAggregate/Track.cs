using SkiAnalyze.Core.Entities.SkiAreaAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.TrackAggregate;

public class Track : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public byte[] FileContents { get; set; } = default!;
    public TrackFileType FileType { get; set; }
    public List<Run> Runs { get; set; } = new();
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double MaxSpeed { get; set; }
    public double AverageSpeed { get; set; }
    public double? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime Date { get; set; }
    public SkiArea? SkiArea { get; set; }
    public long? SkiAreaId { get; set; }

    public AnalysisStatus? AnalysisStatus { get; set; }
}

public enum TrackFileType
{
    Gpx, Fit, Tcx
}