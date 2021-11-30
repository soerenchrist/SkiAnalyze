using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IStatsService
{
    Task<List<BaseStatValue<PisteDifficulty, double>>> GetDifficultyStats(int trackId);
}
