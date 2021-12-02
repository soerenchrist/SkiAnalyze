using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Entities.TrackAggregate.Specifications;
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


    public async Task<List<BaseStatValue<string, int>>> GetGondolaCountByProperty(int trackId, string propertyName)
    {
        var runs = await _runRepository.ListAsync(new GetRunsForTrackSpec(trackId));

        var results = new List<BaseStatValue<string, int>>();
        var dictionary = new Dictionary<string, int>();

        string GetValue(Gondola gondola)
        {
            return propertyName switch
            {
                "bubble" => BoolToString(gondola.Bubble),
                "heating" => BoolToString(gondola.Heating),
                "occupancy" => gondola.Occupancy?.ToString() ?? "unknown",
                _ => gondola.Type
            };
        }

        foreach (var run in runs.Where(x => x.Gondola != null))
        {
            var value = GetValue(run.Gondola!);

            if (dictionary.ContainsKey(value))
            {
                dictionary[value]++;
            }
            else
            {
                dictionary[value] = 1;
            }
        }

        return dictionary
            .Select(x => new BaseStatValue<string, int>(x.Key, x.Value))
            .OrderByDescending(x => x.Value)
            .ToList();
    }

    private string BoolToString(bool? value)
    {
        if (!value.HasValue)
            return "unknown";
        return value.Value ? "yes" : "no";
    }
}
