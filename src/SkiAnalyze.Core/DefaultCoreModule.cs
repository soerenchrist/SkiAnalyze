using Autofac;
using Microsoft.Extensions.Hosting;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Services;
using SkiAnalyze.Core.Services.Stats;
using SkiAnalyze.Core.Workers;

namespace SkiAnalyze.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OsmFileDataProvider>()
            .As<IOsmDataProvider>();

        builder.RegisterType<GondolaSearchService>()
            .As<IGondolaSearchService>();
        builder.RegisterType<PisteSearchService>()
            .As<IPisteSearchService>();
        builder.RegisterType<Analyzer>()
            .As<IAnalyzer>()
            .InstancePerLifetimeScope();

        builder.RegisterType<StatsService>()
            .As<IStatsService>();

        builder.RegisterType<QueuedHostedService>()
            .As<IHostedService>()
            .SingleInstance();
    }
}