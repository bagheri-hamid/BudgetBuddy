using Core.Domain.Dtos.Auth;

namespace Core.Application.Interfaces;

public interface IAuthService
{
    Task<SignUpResponse> SignUp(string username, string? fullName, string email, string password);
    Task<LoginResponse> Login(string username, string password);
}