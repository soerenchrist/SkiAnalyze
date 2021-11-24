﻿using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Common;
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

    public async Task LoadInitialData()
    {
        var alpsNw = new Coordinate
        {
            Latitude = 48.496223f,
            Longitude = 9.334531f
        };
        var alpsSe = new Coordinate
        {
            Latitude = 46.477213f,
            Longitude = 15.674944f
        };
        var gondolaCount = await _appDbContext.Gondolas.CountAsync();
        _logger.LogInformation("Found {Count} gondolas already in db", gondolaCount);
        if (gondolaCount == 0)
        {
            var gondolas = await _dataProvider.GetGondolas(alpsNw, alpsSe);
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
            var pistes = await _dataProvider.GetPistes(alpsNw, alpsSe);
            var pistesList = pistes.ToList();
            var coordinates = pistesList.SelectMany(x => x.Coordinates).ToList();
            _logger.LogInformation("Found {Count} pistes with {CoordCount} nodes from data provider - Inserting them...", pistesList.Count, coordinates.Count);
            await _appDbContext.BulkInsertAsync(pistesList);
            await _appDbContext.BulkInsertAsync(coordinates);
        }
    }
}