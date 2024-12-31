using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseController : ControllerBase;

[Authorize]
public class AuthorizedController : BaseController;