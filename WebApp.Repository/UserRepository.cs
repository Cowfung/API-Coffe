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
    public class UserRepository : Repository<AppUser, string>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByEmail(string email)
        {
            return await _context.Set<AppUser>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
