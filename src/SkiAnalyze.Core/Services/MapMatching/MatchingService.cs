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
        var filteredRuns = runs.Where(x => x.Coordinates.Any()).ToList();
        foreach (var run in filteredRuns)
        {
            run.Number = number;
            if (!run.Downhill)
                run.Gondola = MatchGondola(run, gondolas);
            number++;
        }
        return filteredRuns;
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
        runs.Add(new Run
        {
            Coordinates = segments,
            Downhill = direction == Direction.Down
        });
        return runs;
    }

    private Gondola? MatchGondola(Run run, List<Gondola> gondolas)
    {
        if (run.Coordinates.Count <= 1)
            return null;
        var lastPointOfRun = run.Coordinates.Last();
        var firstPointOfRun = run.Coordinates.First();

        var diffLat = lastPointOfRun.Latitude - firstPointOfRun.Latitude;
        var diffLon = lastPointOfRun.Longitude - firstPointOfRun.Longitude;
        var runDirection = (diffLat, diffLon);

        const double AngleThresh = 0.98;

        foreach(var gondola in gondolas)
        {
            var (startPoint, endPoint) = (gondola.Coordinates.First(), gondola.Coordinates.Last());
            var diffLatGond = endPoint.Latitude - startPoint.Latitude;
            var diffLonGond = endPoint.Longitude - startPoint.Longitude;

            var gondolaDirection = (diffLatGond, diffLonGond);
            var angle = CalculateAngle(runDirection, gondolaDirection);

            if (angle > AngleThresh)
            {
                Console.WriteLine("Yep");
                return gondola;
            }    
        }
        return null;
    }

    private double CalculateAngle((double, double) coord1, (double, double) coord2)
    {
        var (dx1, dy1) = coord1;
        var (dx2, dy2) = coord2;
        return Math.Abs((dx1 * dx2 + dy1 * dy2) / Math.Sqrt((dx1 * dx1 + dy1 * dy1) * (dx2 * dx2 + dy2 * dy2)));
    }

}

internal enum Direction
{
    Up, Down
}

