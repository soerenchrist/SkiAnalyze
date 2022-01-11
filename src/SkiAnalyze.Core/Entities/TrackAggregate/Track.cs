using System.Security.Principal;
using NetTopologySuite.GeometriesGraph;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Entities.TrackAggregate;

public class Track : BaseEntity<int>
{
    public string? Name { get; set; }
    public byte[] FileContents { get; set; } = default!;
    public TrackFileType FileType { get; set; }
    
    private List<Run> _runs = new();
    public List<Run> Runs => _runs.OrderBy(x => x.SortId).ToList();
    public double TotalDistance { get; set; }
    public double TotalElevation { get; set; }
    public int? TotalCalories { get; set; }
    public double MaxSpeed { get; set; }
    public double AverageSpeed { get; set; }
    public double? AverageHeartRate { get; set; }
    public int? MaxHeartRate { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime Date { get; set; }
    public SkiArea? SkiArea { get; set; }
    public long? SkiAreaId { get; set; }

    public AnalysisStatus? AnalysisStatus { get; set; }
    
    
    public void UpdateStats()
    {
        var downhillRuns = Runs.Where(x => x.Downhill).ToList();

        TotalDistance = downhillRuns.Sum(x => x.TotalDistance);
        TotalElevation = downhillRuns.Sum(x => x.TotalElevation);
        MaxSpeed = Runs.Max(x => x.MaxSpeed);
        AverageSpeed = downhillRuns.Average(x => x.AverageSpeed);
        Start = Runs.First().Start;
        End = Runs.Last().End;
        MaxHeartRate = downhillRuns.Max(x => x.MaxHeartRate);
        AverageHeartRate = downhillRuns.Average(x => x.AverageHeartRate);
        TotalCalories = downhillRuns.Sum(x => x.TotalCalories);
        Date = Runs.First().Start.Date;
    }

    public void SetRuns(List<Run> runs)
    {
        _runs = runs;
    }

    public void AddGondola(Gondola gondola, int position)
    {
        var run = new Run
        {
            Downhill = false,
            Coordinates = new List<TrackPoint>(),
            Gondola = gondola,
            SortId = position,
        };
        foreach (var r in _runs)
        {
            if (r.SortId >= run.SortId)
            {
                r.SortId++;
            }
        }
        _runs.Add(run);
        UpdateRunNumbers();
    }

    public void RemoveRun(Run run)
    {
        _runs.Remove(run);
    } 

    public void UpdateRunNumbers()
    {
        var downhillRunNumber = 1;
        var sortId = 1;
        foreach (var run in Runs)
        {
            if (run.Downhill)
            {
                run.Number = downhillRunNumber;
                downhillRunNumber++;
            }

            run.SortId = sortId;
            sortId++;
        }
    }
}

public enum TrackFileType
{
    Gpx, Fit, Tcx
}