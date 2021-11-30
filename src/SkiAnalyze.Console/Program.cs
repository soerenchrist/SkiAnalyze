
using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Data;

if (args.Length < 2)
    return;

string filename = args[0];
string dbFile = args[1];

var connectionString = $"Filename={dbFile}";
var builder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connectionString);
var dbContext = new AppDbContext(builder.Options);

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
