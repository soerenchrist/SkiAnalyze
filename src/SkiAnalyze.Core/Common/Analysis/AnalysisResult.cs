using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Common.Analysis;

public class AnalysisResult : BaseEntity<int>
{
    public Bounds Bounds { get; set; } = null!;
    private List<Run> _runs { get; set; } = new List<Run>();
    public IEnumerable<Run> Runs => _runs.AsReadOnly();
    public Track? Track { get; set; }
    public int TrackId { get; set; }

    public void AddRun(Run run)
    {
        _runs.Add(run);
    }

    public void AddRuns(IEnumerable<Run> runs)
    {
        _runs.AddRange(runs);
    }
}
