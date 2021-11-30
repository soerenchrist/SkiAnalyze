using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.TrackAggregate;
using SkiAnalyze.Core.TrackAggregate.Specifications;
using SkiAnalyze.SharedKernel.Interfaces;

namespace SkiAnalyze.Core.Services.Stats;

public class StatsService : IStatsService
{
    private readonly IReadRepository<TrackPoint> _trackPointRepository;
    private readonly IReadRepository<Run> _runRepository;
    public StatsService(IReadRepository<TrackPoint> trackPointRepository,
        IReadRepository<Run> runRepository)
    {
        _trackPointRepository = trackPointRepository;
        _runRepository = runRepository;
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

    public async Task<List<BaseStatValue<Gondola, int>>> GetTopGondolas(int trackId)
    {
        var runs = await _runRepository.ListAsync(new GetRunsForTrackSpec(trackId));

        var results = new List<BaseStatValue<Gondola, int>>();
        var dictionary = new Dictionary<Gondola, int>();
        foreach (var run in runs.Where(x => x.Gondola != null))
        {
            if (dictionary.ContainsKey(run.Gondola!))
            {
                dictionary[run.Gondola!]++;
            } else
            {
                dictionary[run.Gondola!] = 1;
            }
        }

        return dictionary
            .Select(x => new BaseStatValue<Gondola, int>(x.Key, x.Value))
            .OrderByDescending(x => x.Value)
            .Take(3)
            .ToList();
    }


    public async Task<List<BaseStatValue<string, int>>> GetTopGondolaTypes(int trackId)
    {
        var runs = await _runRepository.ListAsync(new GetRunsForTrackSpec(trackId));

        var results = new List<BaseStatValue<string, int>>();
        var dictionary = new Dictionary<string, int>();
        foreach (var run in runs.Where(x => x.Gondola != null))
        {
            if (dictionary.ContainsKey(run.Gondola!.Type))
            {
                dictionary[run.Gondola!.Type]++;
            }
            else
            {
                dictionary[run.Gondola!.Type] = 1;
            }
        }

        return dictionary
            .Select(x => new BaseStatValue<string, int>(x.Key, x.Value))
            .OrderByDescending(x => x.Value)
            .ToList();
    }
}
