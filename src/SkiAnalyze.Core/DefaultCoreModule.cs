using Autofac;
using SkiAnalyze.Core.Interfaces;
using SkiAnalyze.Core.Services;

namespace SkiAnalyze.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DataInitializer>()
            .As<IDataInitializer>();

        builder.RegisterType<OsmFileDataProvider>()
            .As<IOsmDataProvider>();
    }
}