using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Workers;

namespace SkiAnalyze.Core;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IHostedService, QueuedHostedService>();
    }
}