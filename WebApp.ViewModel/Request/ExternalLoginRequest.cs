using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.ViewModel.Request
{
    public class ExternalLoginRequest
    {
        public string Provider { get; set; } // "Google" hoặc "Facebook"
        public string IdToken { get; set; } // token từ client (Google, Facebook...)
    }
}
