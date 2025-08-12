using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()))
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.ProductSizes.Select(ps => ps.Size.ToString()).ToList()));


            // Trả về Product kèm Category đơn giản
            CreateMap<Product, ProductWithCategoryResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            CreateMap<Category, CategoryShortResponse>(); // cho lồng vào ProductWithCategoryResponse

            // Trả về Category kèm Product list
            CreateMap<Category, CategoryWithProductsResponse>();

            CreateMap<AppUser, UserViewModel>();
            CreateMap<AppUser, UserInfoResponse>();
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.UserDisplayName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : src.GuestName))
                .ForMember(dest => dest.MediaUrls,
               opt => opt.MapFrom(src => src.Media != null ? src.Media.Select(m => m.FilePath).ToList() : new List<string>()))
            .ForMember(dest => dest.ProductId, opt =>
              opt.MapFrom(src => src.ProductId));

        }
    }
}
