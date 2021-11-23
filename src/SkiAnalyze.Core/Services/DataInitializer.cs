

using Microsoft.Extensions.Logging;
using SkiAnalayze.Core.PisteAggregate;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.SharedKernel.Interfaces;
using SkiAnalyze.Core.Common;

namespace SkiAnalyze.Core.Services;

public class DataInitializer : IDataInitializer
{
    private readonly IRepository<Gondola> _gondolaRepository;
    private readonly IRepository<Piste> _pisteRepository;
    private readonly IOsmDataProvider _dataProvider;
    private readonly ILogger<DataInitializer> _logger;

    public DataInitializer(IRepository<Gondola> gondolaRepository,
        IRepository<Piste> pisteRepository,
        IOsmDataProvider dataProvider,
        ILogger<DataInitializer> logger)
    {
        _gondolaRepository = gondolaRepository;
        _pisteRepository = pisteRepository;
        _dataProvider = dataProvider;
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
        var gondolaCount = await _gondolaRepository.CountAsync();
        _logger.LogInformation("Found {Count} gondolas already in db", gondolaCount);
        if (gondolaCount == 0)
        {
            var gondolas = await _dataProvider.GetGondolas(alpsNw, alpsSe);
            var gondolaList = gondolas.ToList();
            _logger.LogInformation("Found {Count} gondolas from data provider - Inserting them...", gondolaList.Count);
            await _gondolaRepository.BulkInsertAsync(gondolaList);
        }

        var pisteCount = await _pisteRepository.CountAsync();
        if (pisteCount == 0)
        {
            var pistes = await _dataProvider.GetPistes(alpsNw, alpsSe);
            var pistesList = pistes.ToList();
            _logger.LogInformation("Found {Count} pistes from data provider - Inserting them...", pistesList.Count);
            await _pisteRepository.BulkInsertAsync(pistesList);
        }
    }
}
