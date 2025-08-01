using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Interface
{
    public interface IRepository<T,K> where T : class
    {
        Task<T>FindByIdAsync(K id,params Expression<Func<T, object>>[]includeProperties);
        IQueryable<T> GetQuery(); // Thêm hàm này để trả về IQueryable<T>
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> FindAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(K id);
        Task RemoveMultipleAsync(List<T> entities);
    }
}
