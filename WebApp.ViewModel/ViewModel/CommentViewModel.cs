using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.ViewModel
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserDisplayName { get; set; } // Tên người dùng hoặc GuestName
        public string Content { get; set; }
        public int Rating { get; set; }

        public string CreatedAt { get; set; }

        public List<string> MediaUrls { get; set; } = new();
    }
}
