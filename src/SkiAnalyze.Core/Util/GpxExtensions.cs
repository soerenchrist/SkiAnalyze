using NetTopologySuite.IO;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Util;

public static class GpxExtensions
{
    public static Bounds GetBounds(this IEnumerable<TrackPoint> points)
    {
        var offsets = 0.01f;
        var minLat = points.Min(x => x.Latitude);
        var minLon = points.Min(x => x.Longitude);
        var maxLat = points.Max(x => x.Latitude);
        var maxLon = points.Max(x => x.Longitude);

        var min = new Coordinate
        {
            Latitude = minLat - offsets,
            Longitude = minLon - offsets
        };
        var max = new Coordinate
        {
            Longitude = maxLon + offsets,
            Latitude = maxLat + offsets
        };
        return new Bounds
        {
            SouthWest = min,
            NorthEast = max
        };
    }

    public static IEnumerable<TrackPoint> ToTrackPoints(this IEnumerable<GpxFile> files) 
    {
        return files
            .SelectMany(x => x.Tracks)
            .SelectMany(x => x.Segments)
            .SelectMany(x => x.Waypoints)
            .Where(x => x.TimestampUtc != null)
            .Select(x => new TrackPoint
            {
                Latitude = (float)x.Latitude.Value,
                Longitude = (float)x.Longitude.Value,
                DateTime = x.TimestampUtc!.Value,
                Elevation = x.ElevationInMeters
            });
    }

    public static double GetLength(this List<Coordinate> coordinates)
    {
        var totalDist = 0.0;
        for(int i = 1; i < coordinates.Count; i++)
        {
            var prev = coordinates[i - 1];
            var current = coordinates[i];

            var distance = prev.DistanceTo(current);
            totalDist += distance;
        }

        return totalDist;
    }

    public static double DistanceTo(this Coordinate coord, Coordinate other)
    {
        var rlat1 = Math.PI * coord.Latitude / 180;
        var rlat2 = Math.PI * other.Latitude / 180;
        var theta = coord.Longitude - other.Longitude;
        var rTheta = Math.PI * theta / 180;
        double dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rTheta);

        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;

        return dist;
    }
}
