using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Products { get;set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
        public string? GuestPhone { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CommentMedia> Media { get; set; } = new List<CommentMedia>();
    }
}
