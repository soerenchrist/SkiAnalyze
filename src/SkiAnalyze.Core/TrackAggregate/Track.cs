using SkiAnalyze.Core.TrackAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.TrackAggregate;

public class Track : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string GpxFileContents { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;
    public List<Run> Runs { get; set; } = new();

    public AnalysisStatus? AnalysisStatus { get; set; }
}
