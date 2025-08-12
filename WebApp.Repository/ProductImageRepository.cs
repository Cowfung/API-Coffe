using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DataBase;
using WebApp.Interface;
using WebApp.Model;

namespace WebApp.Repository
{
    public class ProductImageRepository : Repository<ProductImage, int>, IProductImageRepository
    {
        private readonly AppDbContext _context;

        public ProductImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetImageUrlsByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(p => p.ProductId == productId)
                .Select(p => p.ImageUrl)
                .ToListAsync();
        }
    }
}
