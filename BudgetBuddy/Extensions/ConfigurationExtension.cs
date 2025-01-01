using Core.Application.Settings;

namespace BudgetBuddy.Extensions;

public static class ConfigurationExtension
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}