using EsmeChecker.DataAccess.Data;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;



namespace EsmeChecker.DataAccess.Repository
{
	public class EmploweeRepository : Repository<Emplowee>, IEmploweeRepository
	{
		private readonly PostgreServerDbContext _dbContext;

		public EmploweeRepository(PostgreServerDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
