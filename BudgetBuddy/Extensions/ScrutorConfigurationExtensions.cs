using Core.Application.Interfaces;

namespace BudgetBuddy.Extensions;

public static class ScrutorConfigurationExtensions
{
    public static IServiceCollection AddServicesByScrutor(this IServiceCollection services)
    {
        // Scoped Dependency Registrations
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Core.Application.AssemblyReference), typeof(Infrastructure.AssemblyReference), typeof(WebApi.AssemblyReference))
            .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Core.Application.AssemblyReference), typeof(Infrastructure.AssemblyReference), typeof(WebApi.AssemblyReference))
            .AddClasses(classes => classes.AssignableTo<IScopedDependencyAsSelf>())
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );

        // Singleton Dependency Registrations (Self)
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Core.Application.AssemblyReference), typeof(Infrastructure.AssemblyReference), typeof(WebApi.AssemblyReference))
            .AddClasses(classes => classes.AssignableTo<ISingletonDependencySelf>())
            .AsSelf()
            .WithSingletonLifetime()
        );

        // Transient Dependency Registrations
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Core.Application.AssemblyReference), typeof(Infrastructure.AssemblyReference), typeof(WebApi.AssemblyReference))
            .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime()
        );

        // Singleton Dependency Registrations
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(Core.Application.AssemblyReference), typeof(Infrastructure.AssemblyReference), typeof(WebApi.AssemblyReference))
            .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
            .AsSelfWithInterfaces()
            .WithSingletonLifetime()
        );
        
        return services;
    }
}