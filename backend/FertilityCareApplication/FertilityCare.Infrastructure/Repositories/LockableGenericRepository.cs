using FertilityCare.Domain.Interfaces.Repositoires;
using FertilityCare.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityCare.Infrastructure.Repositories
{
    public class LockableGenericRepository<T, TKey> : ILockableRepository<T, TKey> where T: class
    {

        private FertilityCareDBContext _context;
        
        public LockableGenericRepository(FertilityCareDBContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdWithLockAsync(TKey id)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            if (entityType == null)
            {
                return null;
            }
            var tableName = entityType.GetTableName();
            var keyName = entityType.FindPrimaryKey()?.GetName();

            var sql = $"SELECT * FROM {tableName} WITH (UPDLOCK, ROWLOCK) WHERE {keyName} = @id";
            var param = new SqlParameter("@id", id);

            return await _context.Set<T>().FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }
    }
}
