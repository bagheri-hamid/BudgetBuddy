using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthController : BaseController
{
    [HttpGet]
    public IActionResult Login() => Ok();
}