using System.Text;
using BudgetBuddy.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BudgetBuddy.Extensions;

/// <summary>
/// Provides an extension method to configure JWT Bearer authentication for the application.
/// </summary>
public static class AuthenticationExtension
{
    /// <summary>
    /// Configures JWT Bearer authentication for the application.
    /// </summary>
    /// <param name="services">
    /// The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> 
    /// to which authentication services are added.
    /// </param>
    /// <param name="configuration">
    /// The <see cref="T:Microsoft.Extensions.Configuration.IConfiguration" /> used to retrieve JWT settings.
    /// </param>
    /// <returns>
    /// The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> 
    /// to allow for fluent configuration.
    /// </returns>
    /// <remarks>
    /// This method sets up the application to use JSON Web Tokens (JWT) for authentication. 
    /// It validates tokens against parameters such as the issuer, audience, lifetime, and signing key.
    /// 
    /// The configuration must include a "JwtSettings" section with the following keys:
    /// <list type="bullet">
    ///     <item><description><c>SecretKey</c>: The secret key used to sign JWTs.</description></item>
    ///     <item><description><c>Issuer</c>: The expected issuer of the JWT.</description></item>
    ///     <item><description><c>Audience</c>: The expected audience of the JWT.</description></item>
    /// </list>
    /// </remarks>
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}