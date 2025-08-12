using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Service.Interface;
using WebApp.ViewModel.Common;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.ViewModel;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    
    
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        public ProductController(IProductService productService,IProductImageService productImageService)
        {
            _productService = productService;
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Success(products); // List<ProductDetailResponse>
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return Fail("Không tìm thấy sản phẩm");

            return Success(product); // ProductDetailResponse
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductCreateRequest request)
        {
            var createdProduct = await _productService.Add(request);
            return Success(createdProduct); // ProductDetailResponse
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateRequest request)
        {
            if (id != request.Id)
                return Fail("ID trong URL không khớp với dữ liệu gửi lên");

            var updatedProduct = await _productService.Update(request);
            return Success(updatedProduct); // ProductDetailResponse
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Success("Xoá sản phẩm thành công");
        }
        [HttpGet("hot")]
        public async Task<IActionResult> GetHotProduct()
        {
            var products = await _productService.GetHotProduct();
            return Success(products);
        }
        [HttpGet("{productId}/images")]
        public async Task<IActionResult>GetImages(int productId)
        {
            var images = await _productImageService.GetImageUrlByProductId(productId);
            return Success(images);
        }
        [HttpGet("{id}/sizes")]
        public async Task<IActionResult> GetSizes(int id)
        {
            var sizes = await _productService.GetSizesAsync(id);
            return Success(sizes);
        }
    }
}
