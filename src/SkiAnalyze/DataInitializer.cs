using SkiAnalyze.Data;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace SkiAnalyze;

public class DataInitializer
{
    private readonly AppDbContext _appDbContext;
    private readonly IOsmDataProvider _dataProvider;
    private readonly ILogger<DataInitializer> _logger;

    public DataInitializer(AppDbContext appDbContext,
        IOsmDataProvider dataProvider,
        ILogger<DataInitializer> logger)
    {
        _dataProvider = dataProvider;
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task LoadInitialData(IConfiguration configuration)
    {
        var alpsNe = new Coordinate
        {
            Latitude = configuration.GetValue<float>("OsmBounds:NorthEast:Latitude"),
            Longitude = configuration.GetValue<float>("OsmBounds:NorthEast:Longitude"),
        };
        var alpsSw = new Coordinate
        {
            Latitude = configuration.GetValue<float>("OsmBounds:SouthWest:Latitude"),
            Longitude = configuration.GetValue<float>("OsmBounds:SouthWest:Longitude"),
        };

        var skiAreaCount = await _appDbContext.SkiAreas.CountAsync();
        _logger.LogInformation("Found {Count} ski areas already in db", skiAreaCount);
        if (skiAreaCount == 0)
        {
            var skiAreas = await _dataProvider.GetSkiAreas(alpsNe, alpsSw);
            var skiAreaList = skiAreas.ToList();
            var nodes = skiAreaList.SelectMany(x => x.Nodes).ToList();
            _logger.LogInformation("Found {Count} SkiAreas with {CoordCount} nodes from data provider - Inserting them...", skiAreaList.Count, nodes.Count);
            await _appDbContext.BulkInsertAsync(skiAreaList);
            await _appDbContext.BulkInsertAsync(nodes);
        }

        var gondolaCount = await _appDbContext.Gondolas.CountAsync();
        _logger.LogInformation("Found {Count} gondolas already in db", gondolaCount);
        if (gondolaCount == 0)
        {
            var gondolas = await _dataProvider.GetGondolas(alpsNe, alpsSw);
            var gondolaList = gondolas.ToList();
            var coordinates = gondolaList.SelectMany(x => x.Coordinates).ToList();
            _logger.LogInformation("Found {Count} gondolas with {CoordCount} nodes from data provider - Inserting them...", gondolaList.Count, coordinates.Count);
            await _appDbContext.BulkInsertAsync(gondolaList);
            await _appDbContext.BulkInsertAsync(coordinates);
        }

        var pisteCount = await _appDbContext.Pistes.CountAsync();
        _logger.LogInformation("Found {Count} pistes already in db", pisteCount);
        if (pisteCount == 0)
        {
            var pistes = await _dataProvider.GetPistes(alpsNe, alpsSw);
            var pistesList = pistes.ToList();
            var coordinates = pistesList.SelectMany(x => x.Coordinates).ToList();
            _logger.LogInformation("Found {Count} pistes with {CoordCount} nodes from data provider - Inserting them...", pistesList.Count, coordinates.Count);
            await _appDbContext.BulkInsertAsync(pistesList);
            await _appDbContext.BulkInsertAsync(coordinates);
        }
    }
}
