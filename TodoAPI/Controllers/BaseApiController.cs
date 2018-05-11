using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [Produces("application/json", "application/xml")]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}