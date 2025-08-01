using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Request
{
    public  class UpdateUserRequest
    {
        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
