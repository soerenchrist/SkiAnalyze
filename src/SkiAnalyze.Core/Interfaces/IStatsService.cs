using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.PisteAggregate;

namespace SkiAnalyze.Core.Interfaces;

public interface IStatsService
{
    Task<List<BaseStatValue<PisteDifficulty, double>>> GetDifficultyStats(int trackId);
    Task<List<BaseStatValue<Gondola, int>>> GetTopGondolas(int trackId);
    Task<List<BaseStatValue<string, int>>> GetGondolaCountByProperty(int trackId, string property);
}
