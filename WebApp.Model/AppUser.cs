using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Model
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
