using Core.Application;
using Infrastructure;

namespace BudgetBuddy.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructureServices(configuration)
            .AddApplicationsServices()
            .AddAutoMapper(typeof(WebApi.AssemblyReference).Assembly);
        
        return services;
    }
}