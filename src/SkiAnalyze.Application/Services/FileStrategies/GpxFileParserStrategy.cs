using SkiAnalyze.Application.Services.Gpx;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Interfaces.Common;
using SkiAnalyze.Core.Util;

namespace SkiAnalyze.Application.Services.FileStrategies;

public class GpxFileParserStrategy : ITrackFileParserStrategy
{
    public FileReadResult ReadFileContents(Stream stream)
    {
        var fileLoader = new GpxFileLoader();
        var gpxFile = fileLoader.LoadGpxFile(stream);
        var trackPoints = gpxFile.ToTrackPoints().ToList();
        var runs = SegmentPoints(trackPoints);

        var filteredRuns = runs.Where(x => x.Coordinates.Any()).ToList();

        foreach (var run in filteredRuns)
        {
            CalculateStatistics(run);
        }

        var trackName = gpxFile.Tracks.FirstOrDefault()?.Name ?? "";
        return new FileReadResult(trackName, runs);
    }

    private void CalculateStatistics(Run run)
    {
        run.AverageHeartRate = run.Coordinates.Average(x => x.HeartRate);
        run.MaxHeartRate = run.Coordinates.Max(x => x.HeartRate);
        run.TotalDistance = run.Coordinates
                .Select(x => (ICoordinate)x)
                .ToList()
                .GetLength();
        run.TotalElevation = GetTotalElevation(run);
        run.MaxSpeed = run.Coordinates.GetMaxSpeed();
        run.AverageSpeed = run.Coordinates.GetAverageSpeed();
        run.Start = run.Coordinates.First().DateTime;
        run.End = run.Coordinates.Last().DateTime;
    }

    public double GetTotalElevation(Run run)
    {
        var last = run.Coordinates.Last();
        var first = run.Coordinates.First();
        return last.Elevation - first.Elevation ?? 0;
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

    /// <summary>
    /// Try to find the runs based on the trackpoints. A new run is started when the elevation differs
    /// by 100 meters compared to the previous trackpoint.
    /// </summary>
    /// <param name="points">List of the points of the gpx track</param>
    /// <returns>A list of runs / laps</returns>
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
}
