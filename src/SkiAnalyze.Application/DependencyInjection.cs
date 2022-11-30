using Microsoft.Extensions.DependencyInjection;
using SkiAnalyze.Application.Services;
using SkiAnalyze.Application.Services.Stats;
using SkiAnalyze.Core.Interfaces;

namespace SkiAnalyze.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOsmDataProvider, OsmFileDataProvider>();
        services.AddScoped<IGondolaSearchService, GondolaSearchService>();
        services.AddScoped<IPisteSearchService, PisteSearchService>();
        services.AddScoped<IAnalyzer, Analyzer>();
        services.AddScoped<IStatsService, StatsService>();
    }
}