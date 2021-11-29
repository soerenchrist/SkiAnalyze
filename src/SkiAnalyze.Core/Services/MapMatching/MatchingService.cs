using KdTree;
using KdTree.Math;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.Util;

namespace SkiAnalyze.Core.Services.MapMatching;

public class MatchingService
{
    public IEnumerable<Run> Match(List<Gondola> gondolas, List<Piste> pistes, List<TrackPoint> points)
    {
        var runs = SegmentPoints(points);
        var filteredRuns = runs.Where(x => x.Coordinates.Any()).ToList();

        var pistesTree = BuildPistesKdTree(pistes);
        FindGondolasInBetween(filteredRuns, gondolas);
        var id = 1;
        foreach (var run in filteredRuns)
        {
            run.Id = id;
            if (run.Downhill)
            {
                EstimateDifficulties(run, pistesTree);
            }
            id++;
        }
  
        return filteredRuns;
    }

    private void EstimateDifficulties(Run run, KdTree<double, Piste> pistesTree)
    {
        foreach (var coord in run.Coordinates)
        {
            var nearestNodes = pistesTree.GetNearestNeighbours(new[] { coord.Latitude, coord.Longitude }, 1);
            if (nearestNodes.Any())
            {
                var node = nearestNodes[0];
                coord.Piste = node.Value;
            }
        }
    }

    private KdTree<double, Piste> BuildPistesKdTree(List<Piste> pistes)
    {
        var pistesTree = new KdTree<double, Piste>(2, new DoubleMath());
        foreach (var piste in pistes)
        {
            foreach (var coord in piste.Coordinates)
            {
                pistesTree.Add(new[] { coord.Latitude, coord.Longitude }, piste);
            }
        }
        return pistesTree;
    }

    private void FindGondolasInBetween(List<Run> runs, List<Gondola> gondolas)
    {
        var gondolaStartsTree = new KdTree<double, long>(2, new DoubleMath());
        var gondolaEndsTree = new KdTree<double, long>(2, new DoubleMath());

        foreach (var gondola in gondolas)
        {
            var startOfGondola = gondola.Coordinates.First();
            var endOfGondola = gondola.Coordinates.Last();
            gondolaStartsTree.Add(new[] { startOfGondola.Latitude, startOfGondola.Longitude }, gondola.Id);
            gondolaEndsTree.Add(new[] { endOfGondola.Latitude, endOfGondola.Longitude }, gondola.Id);
        }

        const int searchGondolaCount = 3;

        for (int i = 1; i < runs.Count; i++)
        {
            var previousRun = runs[i - 1];
            var currentRun = runs[i];

            var endOfPrevious = previousRun.Coordinates.Last();
            var startOfCurrent = currentRun.Coordinates.First();

            var gondolaStartNodes = gondolaStartsTree.GetNearestNeighbours(
                new[] { endOfPrevious.Latitude, endOfPrevious.Longitude }, searchGondolaCount);
            var gondolaEndNodes = gondolaEndsTree.GetNearestNeighbours(
                new[] { startOfCurrent.Latitude, startOfCurrent.Longitude }, searchGondolaCount);

            var matchingGondolas = new List<Gondola>();
            foreach (var node in gondolaStartNodes)
            {
                var matchingEnd = gondolaEndNodes.FirstOrDefault(x => x.Value == node.Value);
                if (matchingEnd == null)
                    continue;

                var g = gondolas.First(x => x.Id == node.Value);
                matchingGondolas.Add(g);
            }

            void InsertGondola(Gondola gondola)
            {
                var run = new Run()
                {
                    Downhill = false,
                    Coordinates = new List<TrackPoint>
                    {
                        endOfPrevious,
                        startOfCurrent
                    },
                    Gondola = gondola,
                };
                runs.Insert(i, run);
                i++;
            }

            if (matchingGondolas.Count == 1)
            {
                InsertGondola(matchingGondolas[0]);
            } else if (matchingGondolas.Count > 1)
            {
                var minimumGondola = FindMinimumDistanceGondola(matchingGondolas, startOfCurrent, endOfPrevious);
                InsertGondola(minimumGondola);
            }

        }
    }

    private Gondola FindMinimumDistanceGondola(List<Gondola> matchingGondolas, 
        TrackPoint endPoint, 
        TrackPoint startPoint)
    {
        var min = double.MaxValue;
        Gondola? minGondola = null;
        foreach (var matchingGondola in matchingGondolas)
        {
            var start = matchingGondola.Coordinates.Select(x => new Coordinate
            {
                Latitude = x.Latitude,
                Longitude = x.Longitude,
            }).First();
            var end = matchingGondola.Coordinates.Select(x => new Coordinate
            {
                Latitude = x.Latitude,
                Longitude = x.Longitude,
            }).Last();

            var distanceToStart = start.DistanceTo(startPoint);
            var distanceToEnd = end.DistanceTo(endPoint);
            var totalDist = distanceToEnd + distanceToStart;
            if (totalDist < min)
            {
                min = totalDist;
                minGondola = matchingGondola;
            }
        }
        return minGondola!;
    }

    /// <summary>
    /// Lonely points are mostly single points in the gondola that are pretty irrellevant
    /// </summary>
    private List<TrackPoint> RemoveLonelyPoints(List<TrackPoint> trackPoints)
    {
        const int heightOffset = 30;
        var results = new List<TrackPoint>();
        results.Add(trackPoints[0]);
        results.Add(trackPoints[trackPoints.Count - 1]);
        for (int i = 1; i < trackPoints.Count - 1; i++)
        {
            var previous = trackPoints[i - 1];
            var current = trackPoints[i];
            var next = trackPoints[i + 1];

            if (previous.Elevation + heightOffset < current.Elevation
                && current.Elevation + heightOffset < next.Elevation)
            {
                // this is a lonely point
            }
            else
            {
                results.Add(current);
            }
        }
        return results;
    }

    private List<Run> SegmentPoints(List<TrackPoint> points)
    {
        var filtered = RemoveLonelyPoints(points);
        var runs = new List<Run>();
        var currentSegment = new List<TrackPoint>();
        const int offset = 100;
        for (var i = 1; i < filtered.Count; i++)
        {
            var previous = filtered[i - 1];
            var current = filtered[i];

            currentSegment.Add(previous);

            if (previous.Elevation + offset < current.Elevation)
            {
                runs.Add(new Run
                {
                    Downhill = true,
                    Number = runs.Count + 1,
                    Coordinates = currentSegment
                });
                currentSegment = new List<TrackPoint>();
            }
        }
        return runs;
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

