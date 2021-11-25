using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;

namespace SkiAnalyze.Core.Services.MapMatching;

public class MatchingService
{
    public IEnumerable<Run> Match(List<Gondola> gondolas, List<Piste> pistes, List<TrackPoint> points)
    {
        var runs = SegmentPoints(points);
        var number = 1;
        foreach (var run in runs)
        {
            run.Number = number;
            number++;
        }
        return runs;
    }

    private List<Run> SegmentPoints(List<TrackPoint> points)
    {
        const double offset = 30;
        var direction = Direction.Up;
        var segments = new List<TrackPoint>();
        var runs = new List<Run>();

        double? max = double.MinValue;
        foreach(var point in points)
        {
            if (direction == Direction.Up)
            {
                if (point.Elevation > max)
                {
                    max = point.Elevation;
                }
                // direction changed
                else if (max - point.Elevation > offset)
                {
                    direction = Direction.Down;
                    runs.Add(new Run()
                    {
                        Coordinates = segments,
                        Downhill = false
                    });
                    segments = new List<TrackPoint>();
                }
            } 
            else
            {
                if (point.Elevation < max)
                {
                    max = point.Elevation;
                } else if (point.Elevation - max > offset)
                {
                    direction = Direction.Up;
                    runs.Add(new Run()
                    {
                        Coordinates = segments,
                        Downhill = true
                    });
                    segments = new List<TrackPoint>();
                }
            }

            segments.Add(point);
        }
        return runs;
    }
}

internal enum Direction
{
    Up, Down
}

