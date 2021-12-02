using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.TrackAggregate;

public class Track : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string GpxFileContents { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;
    public List<Run> Runs { get; set; } = new();

    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public double MaxSpeed { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime Date { get; set; }

    public AnalysisStatus? AnalysisStatus { get; set; }
}
