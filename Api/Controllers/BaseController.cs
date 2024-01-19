using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status200OK)]
[Route("[controller]/[action]")]
public class BaseController : ControllerBase
{
}