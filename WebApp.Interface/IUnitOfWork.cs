using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Lưu toàn bộ thay đổi về database
        /// </summary>
        Task CommitAsync();
    }
}
