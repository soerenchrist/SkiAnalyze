using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Services;
using SkiAnalyze.Core.Services.Stats;
using SkiAnalyze.Core.Workers;

namespace SkiAnalyze.Core;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<IOsmDataProvider, OsmFileDataProvider>();
        services.AddScoped<IGondolaSearchService, GondolaSearchService>();
        services.AddScoped<IPisteSearchService, PisteSearchService>();
        services.AddScoped<IAnalyzer, Analyzer>();
        services.AddScoped<IStatsService, StatsService>();
        services.AddSingleton<IHostedService, QueuedHostedService>();
    }
}