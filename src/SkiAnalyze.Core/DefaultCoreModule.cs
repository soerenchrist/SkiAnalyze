using Autofac;
using Microsoft.Extensions.Hosting;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Services;
using SkiAnalyze.Core.Workers;

namespace SkiAnalyze.Core;

public class DefaultCoreModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OsmFileDataProvider>()
            .As<IOsmDataProvider>();

        builder.RegisterType<GondolaSearchService>()
            .As<IGondolaSearchService>();
        builder.RegisterType<PisteSearchService>()
            .As<IPisteSearchService>();
        builder.RegisterType<UserSessionManager>()
            .As<IUserSessionManager>()
            .InstancePerDependency();
        builder.RegisterType<TracksService>()
            .As<ITracksService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<SessionAnalyzer>()
            .As<ISessionAnalyzer>()
            .InstancePerLifetimeScope();

        builder.RegisterType<QueuedHostedService>()
            .As<IHostedService>()
            .SingleInstance();
    }
}