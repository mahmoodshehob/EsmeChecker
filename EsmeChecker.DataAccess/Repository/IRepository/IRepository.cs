using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
		Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int PageSize = 10, int PageNumber = 1);
		Task<T> Get(Expression<Func<T, bool>> filter, string? includeProperties = null,bool tracked = false);
        Task Add(T entity);
		Task Update(T entity);
		Task Remove(T entity);
		Task RemoveRange(IEnumerable<T> entity);
    }
}
