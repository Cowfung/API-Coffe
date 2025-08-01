using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }       
        public string IconPath { get; set; }
        public ICollection<Product> Products { get; set; }

        public Category()
            {
                Products = new List<Product>();
            }
        }
}
