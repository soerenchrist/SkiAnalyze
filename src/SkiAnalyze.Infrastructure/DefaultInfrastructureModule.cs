using Autofac;
using MediatR;
using MediatR.Pipeline;
using SkiAnalyze.Core.GondolaAggregate;
using SkiAnalyze.Infrastructure.Data;
using SkiAnalyze.SharedKernel.Interfaces;
using System.Reflection;

namespace SkiAnalyze.Infrastructure;

public class DefaultInfrastructureModule : Autofac.Module
{
    private readonly bool _isDevelopment = false;
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public DefaultInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        _isDevelopment = isDevelopment;
        var coreAssembly = Assembly.GetAssembly(typeof(Gondola));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));
        if (coreAssembly != null)
        {
            _assemblies.Add(coreAssembly);
        }
        if (infrastructureAssembly != null)
        {
            _assemblies.Add(infrastructureAssembly);
        }
        if (callingAssembly != null)
        {
            _assemblies.Add(callingAssembly);
        }
    }

    protected override void Load(ContainerBuilder builder)
    {
        if (_isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(builder);
        }
        else
        {
            RegisterProductionOnlyDependencies(builder);
        }
        RegisterCommonDependencies(builder);
    }

    private void RegisterCommonDependencies(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(EfRepository<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .InstancePerLifetimeScope();

        builder
            .RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();

        builder.Register<ServiceFactory>(context =>
        {
            var c = context.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });

        var mediatrOpenTypes = new[]
        {
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
            .RegisterAssemblyTypes(_assemblies.ToArray())
            .AsClosedTypesOf(mediatrOpenType)
            .AsImplementedInterfaces();
        }
    }

    private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
    {
        // TODO: Add development only services
    }

    private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
    {
        // TODO: Add production only services
    }
}
