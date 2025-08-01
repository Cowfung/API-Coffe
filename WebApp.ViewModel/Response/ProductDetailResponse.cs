using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Response
{
    public class ProductDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public string Key { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; }
    }
}
