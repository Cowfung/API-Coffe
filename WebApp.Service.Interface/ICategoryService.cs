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
    public interface ICategoryService
    {
        Task<List<CategoryWithProductsResponse>> GetAll();                 // Trả về kèm list sản phẩm nếu cần
        Task<CategoryWithProductsResponse> GetById(int id);               // Trả về chi tiết + sản phẩm
        Task<CategoryWithProductsResponse> Add(CategoryCreateRequest category);
        Task<CategoryWithProductsResponse> Update(CategoryUpdateRequest category);
        Task Delete(int id);
    }
}
