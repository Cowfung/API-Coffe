using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;
using WebApp.ViewModel.ViewModel;

namespace WebApp.Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetHotProduct();
        Task<List<ProductDetailResponse>> GetAll();
        Task<ProductDetailResponse> GetById(int id);
        Task<ProductDetailResponse> Add(ProductCreateRequest product);
        Task<ProductDetailResponse> Update(ProductUpdateRequest product);
        Task<List<string>> GetSizesAsync(int productId);

        Task Delete(int id);
    }
}
