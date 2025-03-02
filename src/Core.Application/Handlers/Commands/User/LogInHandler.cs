using Core.Application.Interfaces;
using Core.Domain.Commands.User;
using Core.Domain.Dtos.Auth;
using MediatR;

namespace Core.Application.Handlers.Commands.User;

public class LogInHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.Login(request.Username, request.Password);
    }
}