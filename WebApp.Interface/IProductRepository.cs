using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Interface
{
    public interface IProductRepository : IRepository<Product,int>
    {
        Task<Product> GetProductWithDetails(int id);
        Task<List<string>> GetSizesByProductIdAsync(int productId);
    }
}
