using Core.Application.Interfaces;
using Core.Domain.Entities;
using Core.Domain.Responses.User;

namespace Core.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService, IScopedDependency
{
    public async Task<SignUpResponse> SignUp(string username, string? fullName, string email, string password)
    {
        return new SignUpResponse
        {
            Message = null
        };
    }
}