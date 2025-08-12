using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;

namespace WebApp.Service.Interface
{
    public interface IUserService
    {
        Task<UserInfoResponse> GetUserInfoAsync(string userId);
        Task<UserInfoResponse> UpdateUserAsync(string userId, UpdateUserRequest request);
        Task<string> UploadAvatarAsync(IFormFile file,string userId);
    }
}
