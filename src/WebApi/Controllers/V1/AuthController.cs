using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class AuthController : BaseController
{
    [HttpGet]
    public IActionResult Login() => Ok();
}