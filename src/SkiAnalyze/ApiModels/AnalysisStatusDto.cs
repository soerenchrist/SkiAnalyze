namespace SkiAnalyze.ApiModels;

public class AnalysisStatusDto
{
    public Guid Id { get; set; }
    public bool IsFinished { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
