using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IStatsService
{
    Task<List<BaseStatValue<PisteDifficulty, double>>> GetDifficultyStats(int trackId);
    Task<List<BaseStatValue<Gondola, int>>> GetTopGondolas(int trackId);
    Task<List<BaseStatValue<string, int>>> GetGondolaCountByProperty(int trackId, string property);
}
