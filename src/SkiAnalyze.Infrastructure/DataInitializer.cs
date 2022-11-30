using System.Diagnostics;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SkiAnalyze.Core.Common;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Data;

namespace SkiAnalyze.Infrastructure;

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

    public async Task LoadInitialData(IConfiguration configuration, Coordinate northEast, Coordinate southWest)
    {
        var stopwatch = Stopwatch.StartNew();

        var skiAreaCount = await _appDbContext.SkiAreas.CountAsync();
        _logger.LogInformation("Found {Count} ski areas already in db", skiAreaCount);
        if (skiAreaCount == 0)
        {
            var skiAreas = await _dataProvider.GetSkiAreas(northEast, southWest);
            var skiAreaList = skiAreas.ToList();
            var nodes = skiAreaList.SelectMany(x => x.Nodes).ToList();
            _logger.LogInformation(
                "Found {Count} SkiAreas with {CoordCount} nodes from data provider - Inserting them...",
                skiAreaList.Count, nodes.Count);
            await _appDbContext.BulkInsertAsync(skiAreaList);
            await _appDbContext.BulkInsertAsync(nodes);
        }

        var gondolaCount = await _appDbContext.Gondolas.CountAsync();
        _logger.LogInformation("Found {Count} gondolas already in db", gondolaCount);
        if (gondolaCount == 0)
        {
            var gondolas = await _dataProvider.GetGondolas(northEast, southWest);
            var gondolaList = gondolas.ToList();
            var coordinates = gondolaList.SelectMany(x => x.Coordinates).ToList();
            _logger.LogInformation(
                "Found {Count} gondolas with {CoordCount} nodes from data provider - Inserting them...",
                gondolaList.Count, coordinates.Count);
            await _appDbContext.BulkInsertAsync(gondolaList);
            await _appDbContext.BulkInsertAsync(coordinates);
        }

        var pisteCount = await _appDbContext.Pistes.CountAsync();
        _logger.LogInformation("Found {Count} pistes already in db", pisteCount);
        if (pisteCount == 0)
        {
            var pistes = await _dataProvider.GetPistes(northEast, southWest);
            var pistesList = pistes.ToList();
            var coordinates = pistesList.SelectMany(x => x.Coordinates).ToList();
            _logger.LogInformation(
                "Found {Count} pistes with {CoordCount} nodes from data provider - Inserting them...", pistesList.Count,
                coordinates.Count);
            await _appDbContext.BulkInsertAsync(pistesList);
            await _appDbContext.BulkInsertAsync(coordinates);
        }

        stopwatch.Stop();
        _logger.LogInformation("Initialization took {ElapsedMillis}", stopwatch.ElapsedMilliseconds);
    }
}