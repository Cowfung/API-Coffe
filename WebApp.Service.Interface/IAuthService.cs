using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;

namespace WebApp.Service.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> ExternalLoginAsync(ExternalLoginRequest request);
        Task LogoutAsync();
    }
}
