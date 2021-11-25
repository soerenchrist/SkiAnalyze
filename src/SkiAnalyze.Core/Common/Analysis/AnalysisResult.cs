namespace SkiAnalyze.Core.Common.Analysis;

public class AnalysisResult
{
    public bool IsRunning { get; set; }
    public Bounds Bounds { get; set; }
    public Guid AnalyisId { get; set; }
    public List<Run> Runs { get; set; } = new List<Run>();
}
