using Core.Domain.Entities;

namespace Core.Application.Interfaces;

public interface IJwtService
{
    /// <summary>
    /// Generates a JWT token for a given user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <returns>JWT token string.</returns>
    string GenerateToken(User user);
}