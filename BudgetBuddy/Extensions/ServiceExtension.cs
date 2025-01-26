using Core.Application;
using Infrastructure;

namespace BudgetBuddy.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServicesByScrutor()
            .AddInfrastructureServices(configuration)
            .AddApplicationServices()
            .AddAutoMapper(typeof(WebApi.AssemblyReference).Assembly);
        
        return services;
    }
}