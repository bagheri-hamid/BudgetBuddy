using Core.Application.Interfaces;
using Core.Domain.Commands.User;
using Core.Domain.Dtos.Auth;
using MediatR;

namespace Core.Application.Handlers.Commands.User;

public class SignUpHandler(IAuthService authService) : IRequestHandler<SignUpCommand, SignUpResponse>
{
    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return await authService.SignUp(request.Username, request.FullName, request.Email, request.Password);
    }
}