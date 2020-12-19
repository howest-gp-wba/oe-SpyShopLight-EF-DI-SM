using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.SpyshopActual.Domain
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(TKey id);
       
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);





    }
}
