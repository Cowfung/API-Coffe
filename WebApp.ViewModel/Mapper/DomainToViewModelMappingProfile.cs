using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.ViewModel.Response;
using WebApp.ViewModel.ViewModel;

namespace WebApp.ViewModel.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            // Dùng trong View Model chung
            CreateMap<Product, ProductViewModel>();

            CreateMap<Category, CategoryViewModel>();

            // Dùng cho chi tiết sản phẩm
            CreateMap<Product, ProductDetailResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // Trả về Product kèm Category đơn giản
            CreateMap<Product, ProductWithCategoryResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Category, CategoryShortResponse>(); // cho lồng vào ProductWithCategoryResponse

            // Trả về Category kèm Product list
            CreateMap<Category, CategoryWithProductsResponse>();

            CreateMap<AppUser, UserViewModel>();
            CreateMap<AppUser, UserInfoResponse>();
        }
    }
}
