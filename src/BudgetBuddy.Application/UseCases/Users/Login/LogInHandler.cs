using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Dtos.Auth;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Users.Login;

public class LogInHandler(IAuthService authService) : IRequestHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.Login(request.Username, request.Password);
    }
}