
using BudgetBuddy.Application.Settings;

namespace BudgetBuddy.Extensions;

/// <summary>
/// Provides an extension method to configure application settings for the application.
/// </summary>
public static class ConfigurationExtension
{
    /// <summary>
    /// Registers strongly-typed configuration settings for the application.
    /// </summary>
    /// <param name="services">
    /// The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> 
    /// to which the configuration settings are added.
    /// </param>
    /// <param name="configuration">
    /// The <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> used to bind settings.
    /// </param>
    /// <returns>
    /// The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> 
    /// to allow for fluent configuration.
    /// </returns>
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}