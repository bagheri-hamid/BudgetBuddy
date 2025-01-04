using Microsoft.Extensions.DependencyInjection;

namespace Core.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationsServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        return services;
    }
}