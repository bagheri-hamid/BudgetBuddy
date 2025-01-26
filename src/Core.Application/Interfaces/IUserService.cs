using Core.Domain.Responses.User;

namespace Core.Application.Interfaces;

public interface IUserService
{
    Task<SignUpResponse> SignUp(string username, string? fullName, string email, string password);
    Task<LoginResponse> Login(string username, string password);
}