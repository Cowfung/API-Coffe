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
    public class CategoryRepository : Repository<Category,int>,ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
