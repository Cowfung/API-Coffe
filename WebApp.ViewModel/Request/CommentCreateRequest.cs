using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Request
{
    public class CommentCreateRequest
    {
        public int ProductId { get; set; }

        public string? UserId { get; set; }

        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestPhone { get; set; }

        public string Content { get; set; }
        public int Rating { get; set; }

        public List<IFormFile>? Files { get; set; } 
    }
}
