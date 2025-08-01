using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.ViewModel.Request;

namespace WebApp.ViewModel.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();

            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();

            CreateMap<RegisterRequest, AppUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            CreateMap<UpdateUserRequest, AppUser>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}
