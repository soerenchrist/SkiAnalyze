
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiAnalyze.Core.Entities.GondolaAggregate;
using SkiAnalyze.Core.Entities.PisteAggregate;
using SkiAnalyze.Core.Entities.SkiAreaAggregate;
using SkiAnalyze.Core.Entities.TrackAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Services;
using SkiAnalyze.Core.Services.FileStrategies;
using SkiAnalyze.Data;
using SkiAnalyze.Infrastructure.Data;

if (args.Length < 2)
    return;

string filename = args[0];
string dbFile = args[1];

var connectionString = $"Filename={dbFile}";
var builder = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connectionString);
var dbContext = new AppDbContext(builder.Options);
var trackRepository = new EfRepository<Track>(dbContext);
var statusRepository = new EfRepository<AnalysisStatus>(dbContext);
var skiAreaRepository = new EfRepository<SkiArea>(dbContext);
var gondolaRepository = new EfRepository<Gondola>(dbContext);
var pisteRepository = new EfRepository<Piste>(dbContext);
var gondolaSearchService = new GondolaSearchService(gondolaRepository);
var pisteSearchService = new PisteSearchService(pisteRepository);

var logger = LoggerFactory.Create(x => x.ClearProviders()).CreateLogger<Analyzer>();
var analyzer = new Analyzer(trackRepository, statusRepository, skiAreaRepository, gondolaSearchService, logger, pisteSearchService);

await analyzer.AnalyzeTrack(1);
/*
var contents = File.ReadAllBytes(filename);

var track = new Track
{
    FileType = TrackFileType.Fit,
    HexColor = "#ff0000",
    Name = "Test",
    FileContents = contents
};
var result = await trackRepository.AddAsync(track);


/*

await analyzer.AnalyzeTrack(2);
*/

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
