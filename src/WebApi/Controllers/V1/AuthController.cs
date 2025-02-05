using Core.Domain.Commands.User;
using Core.Domain.Enums;
using Core.Domain.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace WebApi.Controllers.V1;

public class AuthController(IMediator mediator) : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);
        
        return ResponseHelper.CreateResponse(
            200,
            MessageEnum.Success.GetDescription(),
            true,
            result
        );
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        var result = await mediator.Send(command);
        
        return ResponseHelper.CreateResponse(
            201,
            MessageEnum.CreatedSuccessfully.GetDescription(),
            true,
            result
        );
    }
}