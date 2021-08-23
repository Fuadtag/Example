using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("v{ver:apiVersion}/[controller]")]
    [OpenApiTag("App")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public abstract class BaseController : ControllerBase
    {
    }
}