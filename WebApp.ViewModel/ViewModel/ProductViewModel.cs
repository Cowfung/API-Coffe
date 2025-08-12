using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; } // optional cho update
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string Key { get; set; }
        public int Status { get; set; } // 0 = bình thường, 1 = hot, 2 = ẩn
        public List<string> Images { get; set; } = new();
        public List<string> Sizes { get; set; } = new();
    }
}
