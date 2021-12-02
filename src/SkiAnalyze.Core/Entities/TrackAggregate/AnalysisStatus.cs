using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.TrackAggregate;

public class AnalysisStatus : BaseEntity<Guid>
{
    public bool IsFinished { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public int TrackId { get; set; }
    public Track? Track { get; set; }
}
