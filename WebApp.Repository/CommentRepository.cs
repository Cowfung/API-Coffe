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
    public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetByProductId(int id)
        {
           var reuslt = await _context.Comments
                .Where(c=>c.ProductId == id)
                .Include(c=>c.User)
                .Include(c=>c.Media)
                .OrderByDescending(c=>c.CreatedAt)
                .ToListAsync();
            Console.WriteLine("Comment count: " + reuslt.Count);
            return reuslt;
        }
    }
}
