using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("info")]
        public async Task<IActionResult> GetInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userService.GetUserInfoAsync(userId);
            return Success(user);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var update = await _userService.UpdateUserAsync(userId, request);
            return Success(update);
        }
    }
}
