using OsmSharp;
using OsmSharp.Complete;
using OsmSharp.Streams;
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Common;
using System.Diagnostics;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;

namespace SkiAnalyze.Core.Services;

public class OsmFileDataProvider : IOsmDataProvider
{
    private readonly IOsmFileProvider _osmFileProvider;

    public OsmFileDataProvider(IOsmFileProvider osmFileProvider)
    {
        _osmFileProvider = osmFileProvider;
    }

    public Task<IEnumerable<SkiArea>> GetSkiAreas(Coordinate northWest, Coordinate southEast)
    {
        return Task.Run<IEnumerable<SkiArea>>(() =>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using var fileStream = _osmFileProvider.GetOsmFile();
            var source = new XmlOsmStreamSource(fileStream);

            var (left, top, right, bottom) =
                ((float)northWest.Longitude,
                (float)northWest.Latitude,
                (float)southEast.Longitude,
                (float)southEast.Latitude);

            var filtered = source
                .FilterBox(left, top, right, bottom)
                .Where(x =>
                x.Type == OsmGeoType.Node
                || (
                x.Type == OsmGeoType.Way
                    && x.Tags != null
                    && x.Tags.Contains("landuse", "winter_sports")));

            var complete = filtered.ToComplete();
            var skiAreas = new List<SkiArea>();
            foreach (CompleteWay way in complete.Where(x => x.Type == OsmGeoType.Way))
            {
                var skiarea = SkiArea.FromWay(way);
                skiAreas.Add(skiarea);
            }
            stopWatch.Stop();
            Console.WriteLine("Took {0} seconds", stopWatch.Elapsed.TotalSeconds);
            return skiAreas.Where(x => !string.IsNullOrWhiteSpace(x.Name));
        });
    }

    public Task<IEnumerable<Piste>> GetPistes(Coordinate northWest, Coordinate southEast)
    {
        return Task.Run<IEnumerable<Piste>>(() =>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using var fileStream = _osmFileProvider.GetOsmFile();
            var source = new XmlOsmStreamSource(fileStream);

            var (left, top, right, bottom) =
                ((float)northWest.Longitude, 
                (float)northWest.Latitude, 
                (float)southEast.Longitude, 
                (float)southEast.Latitude);

            var filtered = source
                .FilterBox(left, top, right, bottom)
                .Where(x =>
                x.Type == OsmGeoType.Node
                || (
                x.Type == OsmGeoType.Way
                    && x.Tags != null
                    && x.Tags.Contains("piste:type", "downhill")));
            var complete = filtered.ToComplete();
            var pistes = new List<Piste>();
            foreach (CompleteWay way in complete.Where(x => x.Type == OsmGeoType.Way))
            {
                pistes.Add(Piste.FromWay(way));
            }

            stopWatch.Stop();
            Console.WriteLine("Took {0} seconds", stopWatch.Elapsed.TotalSeconds);
            return pistes;
        });
    }

    public Task<IEnumerable<Gondola>> GetGondolas(Coordinate northWest, Coordinate southEast)
    {
        return Task.Run<IEnumerable<Gondola>>(() =>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using var fileStream = _osmFileProvider.GetOsmFile();
            var source = new XmlOsmStreamSource(fileStream);

            var (left, top, right, bottom) =
                ((float)northWest.Longitude, 
                (float)northWest.Latitude, 
                (float)southEast.Longitude, 
                (float)southEast.Latitude);

            var filtered = source
                .FilterBox(left, top, right, bottom)
                .Where(x =>
                x.Type == OsmGeoType.Node
                || (
                x.Type == OsmGeoType.Way
                    && x.Tags != null
                    && (x.Tags.Contains("aerialway", "chair_lift")
                        || x.Tags.Contains("aerialway", "t-bar")
                        || x.Tags.Contains("aerialway", "gondola")
                        || x.Tags.Contains("aerialway", "drag_lift")
                        || x.Tags.Contains("aerialway", "platter")
                        || x.Tags.Contains("aerialway", "magic_carpet")
                        || x.Tags.Contains("aerialway", "cable_car"))));
            var complete = filtered.ToComplete();
            var gondolas = new List<Gondola>();
            foreach (CompleteWay way in complete.Where(x => x.Type == OsmGeoType.Way))
            {
                gondolas.Add(Gondola.FromWay(way));
            }
            stopWatch.Stop();
            Console.WriteLine("Took {0} seconds", stopWatch.Elapsed.TotalSeconds);
            return gondolas;
        });
    }
}
