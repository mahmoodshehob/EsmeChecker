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
			//var config = await Get(c=> c.Id==1);

			var config = _dbContext.MaxMinConfigs.FirstOrDefault();

            if (config == null) 
			{				
				MaxMinConfig maxMinConfig = new MaxMinConfig();
				maxMinConfig.CreateDate= DateTime.Now;
				await Add(maxMinConfig);
				await Save();

                config = _dbContext.MaxMinConfigs.FirstOrDefault();
            }

			return config;
		}

		public async Task AddMaxMinConfig(MaxMinConfig entity)
		{
			await dbSet.AddAsync(entity);
		}

		public async Task Save()
		{
			await _dbContext.SaveChangesAsync();
		}
	}
}
