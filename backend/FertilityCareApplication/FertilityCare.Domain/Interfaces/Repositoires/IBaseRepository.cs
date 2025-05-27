using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires;

public interface IBaseRepository<T, TKey> where T : class
{
    Task<T> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(TKey id);
    Task<bool> ExistsAsync(TKey id);
}
