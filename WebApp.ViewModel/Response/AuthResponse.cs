using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Response
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool IsExternal { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
