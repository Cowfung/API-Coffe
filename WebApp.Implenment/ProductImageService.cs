using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Interface;
using WebApp.Service.Interface;

namespace WebApp.Service.Implenment
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductImageService(IProductImageRepository productImageRepository, IUnitOfWork unitOfWork)
        {
            _productImageRepository = productImageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<string>> GetImageUrlByProductId(int productId)
        {
            return await _productImageRepository.GetImageUrlsByProductIdAsync(productId);
        }
    }
}
