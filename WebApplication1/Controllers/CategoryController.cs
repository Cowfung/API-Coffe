using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.ViewModel;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var listCategory = await _categoryService.GetAll();
            return Success(listCategory); // List<CategoryWithProductsResponse>
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
                return Fail("Không tìm thấy danh mục");

            return Success(category); // CategoryWithProductsResponse
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryCreateRequest category)
        {
            var newCategory = await _categoryService.Add(category);
            return Success(newCategory); // CategoryWithProductsResponse
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateRequest category)
        {
            if (id != category.Id)
                return Fail("ID không khớp");

            var updatedCategory = await _categoryService.Update(category);
            return Success(updatedCategory); // CategoryWithProductsResponse
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return Success("Xóa danh mục thành công");
        }

        //// 👇 Nếu bạn muốn thêm API tạo Category kèm Products thì thêm bên dưới:
        //[HttpPost("add-with-products")]
        //public async Task<IActionResult> AddCategoryWithProducts([FromBody] CategoryWithProductsCreateRequest request)
        //{
        //    var category = await _categoryService.AddWithProducts(request);
        //    return Success(category);
        //}
    }
}
