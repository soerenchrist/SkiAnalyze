﻿using System.Diagnostics;
using OsmSharp;
using OsmSharp.Complete;
using OsmSharp.Streams;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;
using SkiAnalyze.Core.Interfaces;

namespace SkiAnalyze.Application.Services;

public class OsmFileDataProvider : IOsmDataProvider
{
    private readonly FileStream _fileStream;
    private readonly XmlOsmStreamSource _osmStreamSource;

    public OsmFileDataProvider(IOsmFileProvider osmFileProvider)
    {
        _fileStream = osmFileProvider.GetOsmFile();
        _osmStreamSource = new XmlOsmStreamSource(_fileStream);
    }

    public Task<IEnumerable<SkiArea>> GetSkiAreas(Coordinate northEast, Coordinate southWest)
    {
        return Task.Run(() =>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var (left, top, right, bottom) =
                ((float)southWest.Longitude,
                    (float)northEast.Latitude,
                    (float)northEast.Longitude,
                    (float)southWest.Latitude);

            var filtered = _osmStreamSource
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

    public Task<IEnumerable<Piste>> GetPistes(Coordinate northEast, Coordinate southWest)
    {
        return Task.Run<IEnumerable<Piste>>(() =>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var (left, top, right, bottom) =
                ((float)southWest.Longitude,
                    (float)northEast.Latitude,
                    (float)northEast.Longitude,
                    (float)southWest.Latitude);

            var filtered = _osmStreamSource
                .FilterBox(left, top, right, bottom)
                .Where(x =>
                    x.Type == OsmGeoType.Node
                    || (
                        x.Type == OsmGeoType.Way
                        && x.Tags != null
                        && x.Tags.Contains("piste:type", "downhill")));
            var complete = filtered.ToComplete();
            var pistes = new List<Piste>();
            foreach (var completeOsmGeo in complete.Where(x => x.Type == OsmGeoType.Way))
            {
                var way = (CompleteWay)completeOsmGeo;
                pistes.Add(Piste.FromWay(way));
            }

            stopWatch.Stop();
            Console.WriteLine("Took {0} seconds", stopWatch.Elapsed.TotalSeconds);
            return pistes;
        });
    }

    public Task<IEnumerable<Gondola>> GetGondolas(Coordinate northEast, Coordinate southWest)
    {
        return Task.Run<IEnumerable<Gondola>>(() =>
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var (left, top, right, bottom) =
                ((float)southWest.Longitude,
                    (float)northEast.Latitude,
                    (float)northEast.Longitude,
                    (float)southWest.Latitude);

            var filtered = _osmStreamSource
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

    public void Dispose()
    {
        _osmStreamSource.Dispose();
        _fileStream.Dispose();
    }
}