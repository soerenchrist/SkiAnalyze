using KdTree;
using KdTree.Math;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Util;

namespace SkiAnalyze.Core.Services.MapMatching;

public class MatchingService
{
    public IEnumerable<Run> Match(List<Gondola> gondolas, List<Piste> pistes, List<Run> runs)
    {
        var pistesTree = BuildPistesKdTree(pistes);
        FindGondolasInBetween(runs, gondolas);
        foreach (var run in runs)
        {
            if (run.Downhill)
            {
                EstimateDifficulties(run, pistesTree);
            }
        }
  
        return runs;
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
}