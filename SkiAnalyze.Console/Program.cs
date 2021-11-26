
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.PisteAggregate;
using SkiAnalyze.Core.Services;
using SkiAnalyze.Core.Services.Gpx;
using SkiAnalyze.Core.Services.MapMatching;
using SkiAnalyze.Core.SessionAggregate;
using SkiAnalyze.Core.Util;
using SkiAnalyze.Data;
using SkiAnalyze.Infrastructure.Data;

const string filename = @"C:\Users\cso\Downloads\activity_4421678167.gpx";
const string dbFile = @"C:\Users\cso\dev\SkiAnalyze\src\SkiAnalyze\app.db";

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
var bounds = points.GetBounds();

var gondolas = await gondolaSearchService.GetGondolasInBounds(bounds);

var matcher = new MatchingService();
var result = matcher.Match(gondolas, pistes, points.ToList());

var up = result.Where(x => !x.Downhill);
var down = result.Where(x => x.Downhill);

Console.WriteLine("Found {0} runs, {1} up and {2} down", result.Count(), up.Count(), down.Count());
Console.WriteLine("{0} out of {1} gondolas matched", down.Count(x => x.Gondola != null), down.Count());

var count = 0;
foreach (var downhill in down)
{
    count++;
    var firstPoint = downhill.Coordinates.First();
    var lastPoint = downhill.Coordinates.Last();
    var elevationDiff = firstPoint.Elevation - lastPoint.Elevation;
    Console.WriteLine("{0}: First: {1}, Last: {2}. ElevationDiff = {3}, Bottom = {4}", count, firstPoint.DateTime, lastPoint.DateTime, elevationDiff, lastPoint.Elevation);
}