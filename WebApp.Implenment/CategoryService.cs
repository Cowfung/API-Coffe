using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Interface;
using WebApp.Model;
using WebApp.Service.Interface;
using WebApp.ViewModel.Request;
using WebApp.ViewModel.Response;
using WebApp.ViewModel.ViewModel;

namespace WebApp.Service.Implenment
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryWithProductsResponse> Add(CategoryCreateRequest request)
        {
            var category = _mapper.Map<Category>(request);

            // Nếu bạn không cho phép tạo Category kèm Products thì có thể bỏ đoạn dưới
            if (category.Products != null)
            {
                foreach (var product in category.Products)
                {
                    product.Category = category;
                }
            }

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();

            var addedCategory = await _categoryRepository.FindSingleAsync(x => x.Id == category.Id, x => x.Products);
            return _mapper.Map<CategoryWithProductsResponse>(addedCategory);
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.FindSingleAsync(x => x.Id == id);
            if (category == null)
                throw new Exception("Category not found");

            await _categoryRepository.RemoveAsync(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<CategoryWithProductsResponse>> GetAll()
        {
            var list = await _categoryRepository.FindAllAsync(x => x.Products);
            return _mapper.Map<List<CategoryWithProductsResponse>>(list);
        }

        public async Task<CategoryWithProductsResponse> GetById(int id)
        {
            var category = await _categoryRepository.FindSingleAsync(x => x.Id == id, x => x.Products);
            if (category == null)
                throw new Exception("Category not found");

            return _mapper.Map<CategoryWithProductsResponse>(category);
        }

        public async Task<CategoryWithProductsResponse> Update(CategoryUpdateRequest request)
        {
            var existing = await _categoryRepository.FindSingleAsync(x => x.Id == request.Id, x => x.Products);
            if (existing == null)
                throw new Exception("Category not found");

            _mapper.Map(request, existing);
            await _categoryRepository.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CategoryWithProductsResponse>(existing);
        }



    }
}
