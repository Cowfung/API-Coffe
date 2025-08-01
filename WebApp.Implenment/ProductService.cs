using AutoMapper;
using WebApp.Infrastructure.enumExtension;
using WebApp.Interface;
using WebApp.Model;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;
using WebApp.ViewModel.ViewModel;

namespace WebApp.Service.Implenment
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductDetailResponse>> GetAll()
        {
            var productList = await _productRepository.FindAllAsync(x => x.Category);
            return _mapper.Map<List<ProductDetailResponse>>(productList);
        }

        public async Task<ProductDetailResponse> GetById(int id)
        {
            var product = await _productRepository.FindSingleAsync(x => x.Id == id, x => x.Category);
            if (product == null)
                throw new Exception("Product not found");

            return _mapper.Map<ProductDetailResponse>(product);
        }

        public async Task<ProductDetailResponse> Add(ProductCreateRequest request)
        {
            var newProduct = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(newProduct);
            await _unitOfWork.CommitAsync();

            // Lấy lại để map đầy đủ thông tin (bao gồm Category nếu cần)
            var addedProduct = await _productRepository.FindSingleAsync(x => x.Id == newProduct.Id, x => x.Category);
            return _mapper.Map<ProductDetailResponse>(addedProduct);
        }

        public async Task<ProductDetailResponse> Update(ProductUpdateRequest request)
        {
            var existingProduct = await _productRepository.FindSingleAsync(x => x.Id == request.Id, x => x.Category);
            if (existingProduct == null)
                throw new Exception("Product not found");

            _mapper.Map(request, existingProduct);
            await _productRepository.UpdateAsync(existingProduct);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ProductDetailResponse>(existingProduct);
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.FindSingleAsync(x => x.Id == id);
            if (product == null)
                throw new Exception("Product not found");

            await _productRepository.RemoveAsync(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProductViewModel>> GetHotProduct()
        {
            var hotProducts = await _productRepository.FindAllAsync(p=>p.Status == ProductStatus.Hot );
            return _mapper.Map<List<ProductViewModel>>(hotProducts);
        }
    }
}
