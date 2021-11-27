﻿using NetTopologySuite.IO;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces.Common;

namespace SkiAnalyze.Core.Util;

public static class GpxExtensions
{
    public static Bounds GetBounds(this IEnumerable<ICoordinate> points)
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

    public static double GetLength(this List<ICoordinate> coordinates)
    {
        var totalDist = 0.0;
        for (int i = 1; i < coordinates.Count; i++)
        {
            var prev = coordinates[i - 1];
            var current = coordinates[i];

            var distance = prev.DistanceTo(current);
            totalDist += distance;
        }

        return totalDist;
    }


    public static double DistanceTo(this ICoordinate coord, ICoordinate other)
    {
        double dlon = Radians(other.Longitude - coord.Longitude);
        double dlat = Radians(other.Latitude - coord.Latitude);

        double a = (Math.Sin(dlat / 2) * Math.Sin(dlat / 2)) + Math.Cos(Radians(coord.Latitude)) 
            * Math.Cos(Radians(other.Latitude)) * (Math.Sin(dlon / 2) * Math.Sin(dlon / 2));
        double angle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return angle * Radius * 1000;
    }

    public static double Radians(double x)
    {
        return x * Math.PI / 180;
    }
    const double Radius = 6371;
}