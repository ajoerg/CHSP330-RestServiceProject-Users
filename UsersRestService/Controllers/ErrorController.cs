using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsersRestService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("/error/{code}")]
        public IActionResult Error(int code)
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return new ObjectResult(new ApiResponse(code, feature.Error.Message));
        }
    }
}
