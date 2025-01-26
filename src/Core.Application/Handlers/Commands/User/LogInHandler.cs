using Core.Application.Interfaces;
using Core.Domain.Commands.User;
using Core.Domain.Responses.User;
using MediatR;

namespace Core.Application.Handlers.Commands.User;

public class LogInHandler(IUserService userService) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await userService.Login(request.Username, request.Password);
    }
}