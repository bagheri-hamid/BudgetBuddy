using BudgetBuddy.Application;
using BudgetBuddy.Infrastructure;

namespace BudgetBuddy.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddServicesByScrutor()
            .AddInfrastructureServices(configuration)
            .AddApplicationServices()
            .AddAutoMapper(typeof(Api.AssemblyReference).Assembly);
        
        return services;
    }
}