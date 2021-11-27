
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Core.Common.Analysis;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.Services;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.Services.MapMatching;
using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.Core.Util;
using SkiAnalyze.Data;
using SkiAnalyze.Infrastructure.Data;
using System.Text.Json;

if (args.Length < 2)
    return;

string filename = args[0];
string dbFile = args[1];

var connectionString = $"Filename={dbFile}";
var builder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connectionString);
var dbContext = new AppDbContext(builder.Options, null);
var repo = new EfRepository<Gondola>(dbContext);
var gondolaSearchService = new GondolaSearchService(repo);

var content = await File.ReadAllTextAsync(filename);
var track = new Track
{
    GpxFileContents = content
};

var tracks = new List<Track> { track };

var fileLoader = new GpxFileLoader();
var gpxFiles = fileLoader.LoadGpxFiles(tracks);


var pistes = new List<Piste>();

var points = gpxFiles.ToTrackPoints();

var json = JsonSerializer.Serialize(points);
File.WriteAllText("trackpoints.json", json);

var bounds = points.GetBounds();

var gondolas = await gondolaSearchService.GetGondolasInBounds(bounds);

var matcher = new MatchingService();
var result = matcher.Match(gondolas, pistes, points.ToList());

var up = result.Where(x => !x.Downhill);
var down = result.Where(x => x.Downhill);

Console.WriteLine("Found {0} runs, {1} up and {2} down", result.Count(), up.Count(), down.Count());
Console.WriteLine("{0} out of {1} gondolas matched", up.Count(x => x.Gondola != null), up.Count());

var count = 0;
foreach (var downhill in down)
{
    count++;
    var firstPoint = downhill.Coordinates.First();
    var lastPoint = downhill.Coordinates.Last();
    var elevationDiff = firstPoint.Elevation - lastPoint.Elevation;

    if (downhill.Number == 2)
        CalculateSpeeds(downhill);
    Console.WriteLine("{0}: First: {1}, Last: {2}. ElevationDiff = {3}, Bottom = {4}", count, firstPoint.DateTime, lastPoint.DateTime, elevationDiff, lastPoint.Elevation);
}

void CalculateSpeeds(Run downhill)
{
    for (int i = 1; i < downhill.Coordinates.Count; i++)
    {
        var prev = downhill.Coordinates[i - 1];
        var current = downhill.Coordinates[i];

        var distance = prev.DistanceTo(current);
        var time = current.DateTime - prev.DateTime;
        var speed = distance / time.TotalSeconds;

        Console.WriteLine("Speed: {0}", speed);
    }
}

Console.ReadKey();