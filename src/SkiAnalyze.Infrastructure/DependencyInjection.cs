using Microsoft.EntityFrameworkCore;
using SkiAnalyze.Infrastructure.Backgrounding;
using SkiAnalyze.Infrastructure.Data;
using SkiAnalyze.SharedKernel.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Data;
using SkiAnalyze.Infrastructure.File;

namespace SkiAnalyze.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString, string osmPath)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
        });
        var backgroundTaskQueue = new BackgroundTaskQueue(100);
        services.AddSingleton<IBackgroundTaskQueue>(backgroundTaskQueue);
        services.AddScoped<IOsmFileProvider>(_ => new OsmFileProvider(osmPath));
    }
}