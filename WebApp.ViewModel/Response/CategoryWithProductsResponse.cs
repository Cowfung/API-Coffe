using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Response
{
    public class CategoryWithProductsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string IconPath { get; set; }
        public List<ProductDetailResponse> Products { get; set; }
    }
}
