using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApp.Model;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;
using WebApp.ViewModel.ViewModel;

namespace WebApp.Service.Implenment
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<AppUser> userManager, IConfiguration config, SignInManager<AppUser> signInManager ,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
           var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new Exception("Invaild email or password");
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new Exception("Invaild email or password");
            return await GenerateJwtToken(user,request.RememberMe);

        }
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = _mapper.Map<AppUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception("Register failed");
            return await GenerateJwtToken(user);
            
        }
        public async Task<AuthResponse> ExternalLoginAsync(ExternalLoginRequest request)
        {
            AppUser user = null;
            string email = null;
            string fullname = null;
            string providerKey = null;
            if (request.Provider == "Google")
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
                email = payload.Email;
                fullname = payload.Name;
                providerKey = payload.Subject;
            }
            else if (request.Provider == "Facebook")
            {
                var client = new HttpClient();
                var result = await client.GetStringAsync(
                    $"https://graph.facebook.com/me?fields=id,name,email&access_token={request.IdToken}");
                var facebookData = JsonSerializer.Deserialize<FacebookUserInfo>(result);
                email = facebookData.Email;
                fullname = facebookData.Name;
                providerKey = facebookData.Id;
            }
            if (string.IsNullOrEmpty(email))
                throw new Exception("Cannot get email from external provider");

            user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    FullName = fullname,
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                    throw new Exception($"Login with {request.Provider} failed");

                await _userManager.AddLoginAsync(user, new UserLoginInfo(
                    request.Provider, providerKey, request.Provider));
            }
            return await GenerateJwtToken(user);
        }

        public  Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt"); // 👈 xóa JWT trong cookie
            return Task.CompletedTask;
        }
        private async Task<AuthResponse> GenerateJwtToken(AppUser user , bool rememberMe = false)
        {
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var expire = rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(2);


            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: expire,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // 👉 Set cookie ở đây
            //_httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", tokenString, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.Strict,
            //    Expires = expire
            //});

            return new AuthResponse
            {
                Token = tokenString,
                ExpireAt = token.ValidTo,
                Email = user.Email,
                FullName = user.FullName
            };
        }


    }
}
