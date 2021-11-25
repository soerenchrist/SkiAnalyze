using Autofac;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Services;

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
            .As<IUserSessionManager>();
        builder.RegisterType<TracksService>()
            .As<ITracksService>();

        builder.RegisterType<SessionAnalyzer>()
            .As<ISessionAnalyzer>();
    }
}