using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using EsmeChecker.DataAccess.Data;
using EsmeChecker.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Build.Framework;

namespace EsmeChecker.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PostgreServerDbContext _dbContext;
        internal DbSet<T> dbSet;


        public Repository(PostgreServerDbContext dbContext)
        {
            _dbContext = dbContext;

            this.dbSet = _dbContext.Set<T>();

            //_dbContext.Products.Include(u => u.Category).Include(u => u.CategoryId);

        }

        public async Task Add(T entity)
        {
            dbSet.Add(entity);
			await _dbContext.SaveChangesAsync();

		}

		public async Task<T> Get(Expression<Func<T, bool>> filter , string? includeProperties = null, bool tracked = false)
        {

            IQueryable<T> query = dbSet;

            if (tracked)
            {
                query =   query.Where(filter);
            }
            else
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }


            var result = await query.FirstOrDefaultAsync().ConfigureAwait(false);
            return  result;
        }


		public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int PageSize = 10, int PageNumber = 1)
        {
			try
			{
				PageNumber = PageNumber <= 0 ? 1 : PageNumber;

				IQueryable<T> query = dbSet;
				if (filter != null)
				{
					query = query.Where(filter).Skip((PageNumber - 1) * PageSize).Take(PageSize);
				}
				else
				{
					query = query.Skip((PageNumber - 1) * PageSize).Take(PageSize);
				}

				if (includeProperties != null)
				{
					foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
					{
						query = query.Include(includeProp).Skip((PageNumber - 1) * PageSize).Take(PageSize);
					}
				}
				return await query.ToListAsync();
			}
			catch (Exception ex)
			{
                string message = ex.Message;
				return Enumerable.Empty<T>();
			}
		}
        
        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
			await _dbContext.SaveChangesAsync();

		}

		public async Task RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
			await _dbContext.SaveChangesAsync();
		}

        public async Task Update(T entity)
        {
            dbSet.Update(entity);
			await _dbContext.SaveChangesAsync();
		}

    }
}
