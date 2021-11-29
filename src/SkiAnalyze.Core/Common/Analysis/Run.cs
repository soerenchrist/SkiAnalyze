using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Util;
using SkiAnalyze.SharedKernel;

namespace SkiAnalyze.Core.Common.Analysis;

public class Run : BaseEntity<int>
{
    public int Number { get; set; }
    public bool Downhill { get; set; }
    public Gondola? Gondola { get; set; }
    public long? GondolaId { get; set; }
    public List<TrackPoint> Coordinates { get; set; } = new();
    public string Color { get; set; } = string.Empty;
    public Guid AnalysisId { get; set; }
    public AnalysisResult? AnalysisResult { get; set; }

    public double TotalDistance
    {
        get
        {
            if (Coordinates.Count < 2)
                return 0;
            return Coordinates
                .Select(x => (ICoordinate) x)
                .ToList()
                .GetLength();
        }
    }
    public double TotalElevation
    {
        get
        {
            if (Coordinates.Count < 2)
                return 0;
            var last = Coordinates.Last();
            var first = Coordinates.First();
            return last.Elevation - first.Elevation ?? 0;
        }
    }

    public double AverageSpeed => Coordinates.GetAverageSpeed();
    public double MaxSpeed => Coordinates.GetMaxSpeed();
}