using EsmeChecker.DataAccess.Data;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;



namespace EsmeChecker.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly PostgreServerDbContext _dbContext;

		public CategoryRepository(PostgreServerDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
