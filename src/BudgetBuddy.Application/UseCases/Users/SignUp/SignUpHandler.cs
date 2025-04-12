using BudgetBuddy.Application.Interfaces;
using BudgetBuddy.Domain.Dtos.Auth;
using MediatR;

namespace BudgetBuddy.Application.UseCases.Users.SignUp;

public class SignUpHandler(IAuthService authService) : IRequestHandler<SignUpCommand, SignUpResponse>
{
    public async Task<SignUpResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return await authService.SignUp(request.Username, request.FullName, request.Email, request.Password);
    }
}