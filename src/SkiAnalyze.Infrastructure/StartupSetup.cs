using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SkiAnalyze.Data;
using SkiAnalyze.Infrastructure.File;
using SkiAnalyze.Core.Interfaces;

namespace SkiAnalyze.Infrastructure;

public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(connectionString));

    public static void AddOsmFile(this IServiceCollection services, string path) =>
        services.AddScoped<IOsmFileProvider>(_ => new OsmFileProvider(path));
}
