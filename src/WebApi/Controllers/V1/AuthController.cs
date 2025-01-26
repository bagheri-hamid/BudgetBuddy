using Core.Domain.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}