using Core.Domain.Responses.User;

namespace Core.Application.Interfaces;

public interface IUserService
{
    public Task<SignUpResponse> SignUp(string username, string? fullName, string email, string password);
}