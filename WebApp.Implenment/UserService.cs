using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Infrastructure;
using WebApp.Interface;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;

namespace WebApp.Service.Implenment
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string _uploadRootPath;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository,IMapper mapper, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadRootPath = options.Value.UploadRootPath;
        }
        public async Task<UserInfoResponse> GetUserInfoAsync(string userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            return _mapper.Map<UserInfoResponse>(user);
        }


        public async Task<UserInfoResponse> UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            _mapper.Map(request, user);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<UserInfoResponse>(user);
        }
        public async Task<string> UploadAvatarAsync(IFormFile file,string userId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(ext))
                throw new ArgumentException("File type not allowed");

            var uploadFolder = Path.Combine(_uploadRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var fileName = Guid.NewGuid() + ext;
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn URL public (frontend có thể truy cập)
            var relativePath =  $"/uploads/avatars/{fileName}";
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null) throw new Exception("User  not found");
            user.AvatarUrl = relativePath;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return relativePath;
        }
    }
}
