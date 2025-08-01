using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Request
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; } // Cần Id để biết cập nhật sản phẩm nào
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
         public string ImagePath { get; set; }
        public string Key { get; set; }
        public int Status { get; set; }
    }
}
