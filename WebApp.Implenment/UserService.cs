using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
