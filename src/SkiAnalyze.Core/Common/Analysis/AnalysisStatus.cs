using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Common.Analysis;

public class AnalysisStatus : BaseEntity<Guid>
{
    public Guid UserSessionId { get; set; }
    public UserSession? UserSession { get; set; }
    public bool IsFinished { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}
