using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class CommentMedia
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public Comment comment { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}
