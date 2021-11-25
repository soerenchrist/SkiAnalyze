using Ardalis.Result;
using SkiAnalyze.Core.Common.Analysis;

namespace SkiAnalyze.Core.Interfaces;

public interface ISessionAnalyzer
{
    Task<Result<AnalysisResult>> StartAnalysis(Guid userSessionId);
}
