using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Infrastructure.enumExtension
{
    public enum ProductStatus
    {
        Normal = 0,         // Sản phẩm bình thường
        Hot = 1,            // Sản phẩm hot, bán chạy
        Hidden = 2,         // Không hiển thị ở đâu cả
        OutOfStock = 3,     // Hết hàng
        Archived = 4        // Ngưng bán, không quay lại

    }
}
