
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Core.Common;
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

var userSessionRepo = new EfRepository<UserSession>(dbContext);
var sessionManager = new UserSessionManager(userSessionRepo);

var session = await sessionManager.GetUserSession(Guid.Parse("6117d8f0-c725-49be-9729-4c4979c08997"));
if (session == null)
    return;

//var analysis = new AnalysisResult
//{
//    Bounds = new Bounds
//    {
//        NorthEast = new Coordinate(),
//        SouthWest = new Coordinate()
//    },
//    Runs = new List<Run>
//    {
//        new Run
//        {
//            Color = "#ff0000",
//            Downhill = true,
//            Number = 1,
//            Gondola = dbContext.Gondolas.First(),
//            Coordinates = new List<TrackPoint>
//            {
//                new TrackPoint
//                {
//                    Latitude = 1,
//                    Longitude = 1,
//                    DateTime = DateTime.Now,
//                    Elevation = 1000,
//                },

//                new TrackPoint
//                {
//                    Latitude = 2,
//                    Longitude = 2,
//                    DateTime = DateTime.Now,
//                    Elevation = 2000,
//                    Piste = dbContext.Pistes.First()
//                }
//            }
//        }
//    }
//};

//var track = session.Tracks.First();
//track.AnalysisResult = analysis;


//await sessionManager.UpdateUserSession(session);
