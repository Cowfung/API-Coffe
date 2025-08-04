using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using WebApp.Model;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
      
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
           
            var response = await _authService.LoginAsync(loginRequest);
            return Success(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var response = await _authService.RegisterAsync(registerRequest);
            //SetJwtCookie(response.Token ,response.ExpireAt);
            return Success(response);
        }
        [HttpPost("external-login")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginRequest externalLoginRequest)
        {
           
            var respone = await _authService.ExternalLoginAsync(externalLoginRequest);
            //SetJwtCookie(respone.Token,respone.ExpireAt);
            return Success(respone);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Success("đăng xuất thành công");
        }
       
       
    }
}
