using EsmeChecker.DataAccess.Data;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;



namespace EsmeChecker.DataAccess.Repository
{
	public class MaxMinConfigRepository : Repository<MaxMinConfig>, IMaxMinConfigRepository
	{
		private readonly PostgreServerDbContext _dbContext;

		public MaxMinConfigRepository(PostgreServerDbContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<MaxMinConfig> GetMaxMinConfig()
		{
			var config = await Get(c=> c.Id==1);
			if (config == null) 
			{				
				MaxMinConfig maxMinConfig = new MaxMinConfig();
				maxMinConfig.CreateDate= DateTime.Now;
				await Add(maxMinConfig);
				Save();

				config = await Get(c => c.Id == 1);

			}



			return config;
		}

		public async Task AddMaxMinConfig(MaxMinConfig entity)
		{
			dbSet.Add(entity);
		}
		public void Save()
		{
			_dbContext.SaveChanges();
		}
	}
}
