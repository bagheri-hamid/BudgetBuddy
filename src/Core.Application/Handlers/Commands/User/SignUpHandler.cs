using Core.Domain.Commands.User;
using MediatR;

namespace Core.Application.Handlers.Commands.User;

public class SignUpHandler : IRequestHandler<SignUpCommand, string>
{
    public async Task<string> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return "success";
    }
}