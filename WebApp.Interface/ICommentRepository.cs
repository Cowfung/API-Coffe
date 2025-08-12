using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;

namespace WebApp.Interface
{
    public interface ICommentRepository : IRepository<Comment,int>
    {
        Task<List<Comment>> GetByProductId(int id);
    }
}
