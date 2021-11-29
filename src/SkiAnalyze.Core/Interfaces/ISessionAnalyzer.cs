namespace SkiAnalyze.Core.Interfaces;

public interface ISessionAnalyzer
{
    Task AnalyzeSession(Guid userSessionId, Guid analysisId);
}
