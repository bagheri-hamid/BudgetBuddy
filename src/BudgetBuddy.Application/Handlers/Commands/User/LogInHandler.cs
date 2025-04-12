using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Commands.User;
using BudgetBuddy.Domain.Dtos.Auth;
using MediatR;

namespace BudgetBuddy.Application.Handlers.Commands.User;

public class LogInHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.Login(request.Username, request.Password);
    }
}