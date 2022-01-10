using SkiAnalyze.Core.Entities.TrackAggregate;

namespace SkiAnalyze.Core.Util;

public static class TrackExtensions
{
    
    public static void UpdateStats(this Track track)
    {
        var downhillRuns = track.Runs.Where(x => x.Downhill).ToList();

        track.TotalDistance = downhillRuns.Sum(x => x.TotalDistance);
        track.TotalElevation = downhillRuns.Sum(x => x.TotalElevation);
        track.MaxSpeed = track.Runs.Max(x => x.MaxSpeed);
        track.AverageSpeed = downhillRuns.Average(x => x.AverageSpeed);
        track.Start = track.Runs.First().Start;
        track.End = track.Runs.Last().End;
        track.MaxHeartRate = downhillRuns.Max(x => x.MaxHeartRate);
        track.AverageHeartRate = downhillRuns.Average(x => x.AverageHeartRate);
        track.TotalCalories = downhillRuns.Sum(x => x.TotalCalories);
        track.Date = track.Runs.First().Start.Date;
    }

    public static void UpdateRunNumbers(this Track track)
    {
        var number = 1;
        foreach (var run in track.Runs.Where(x => x.Downhill))
        {
            run.Number = number;
            number++;
        }
    }
}