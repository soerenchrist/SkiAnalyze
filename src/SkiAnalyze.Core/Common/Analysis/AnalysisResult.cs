using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Common.Analysis;

public class AnalysisResult : BaseEntity<int>
{
    public Bounds Bounds { get; set; } = null!;
    public List<Run> Runs { get; set; } = new List<Run>();

    public Track? Track { get; set; }
    public int TrackId { get; set; }
}
