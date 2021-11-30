using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.TrackAggregate.Specifications;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Core.Services.Stats;

public class StatsService : IStatsService
{
    private readonly IReadRepository<TrackPoint> _trackPointRepository;
    public StatsService(IReadRepository<TrackPoint> trackPointRepository)
    {
        _trackPointRepository = trackPointRepository;
    }

    public async Task<List<BaseStatValue<PisteDifficulty, double>>> GetDifficultyStats(int trackId)
    {
        var points = await _trackPointRepository.ListAsync(new GetTrackPointsForTrackSpec(trackId));

        var results = new List<BaseStatValue<PisteDifficulty, double>>();
        foreach(PisteDifficulty difficulty in Enum.GetValues(typeof(PisteDifficulty)))
        {
            var count = points.Count(x => x.Piste?.Difficulty == difficulty);
            var percentage = (double) count / points.Count;
            results.Add(new BaseStatValue<PisteDifficulty, double>(difficulty, percentage));
        }

        return results;
    }
}
