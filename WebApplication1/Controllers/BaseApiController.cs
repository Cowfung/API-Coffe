using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModel.Common;

namespace WebApplication1.Controllers
{
    [Authorize]
    
    public class BaseApiController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string message = "")
        {
            var response = ApiResponse<T>.SuccessResponse(data, message);
            return Ok(response);
        }

        protected IActionResult Fail(string message)
        {
            var response = ApiResponse<string>.FailResponse(message);
            return BadRequest(response);
        }
    }
}
