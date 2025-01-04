using Core.Domain.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpGet]
    public IActionResult Login() => Ok();

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}