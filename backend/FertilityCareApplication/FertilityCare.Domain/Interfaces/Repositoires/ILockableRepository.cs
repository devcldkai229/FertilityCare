using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Domain.Interfaces.Repositoires
{
    public interface ILockableRepository<T, TKey> where T: class
    {
        Task<T?> GetByIdWithLockAsync(TKey id);
    }
}
