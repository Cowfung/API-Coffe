using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Request
{
    public class CategoryCreateRequest
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string IconPath { get; set; }
    }
}
