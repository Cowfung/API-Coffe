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
    public class ProductRepository : Repository<Product,int>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Product> GetProductWithDetails(int id)
        {
            return await _context.Products.Include(p => p.Category).Include(p => p.ProductImages).Include(p => p.ProductSizes).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<string>> GetSizesByProductIdAsync(int productId)
        {
            return await _context.ProductSizes.Where(ps=>ps.ProductId == productId).Select(ps=>ps.Size.ToString()).ToListAsync();
        }
    }
}
