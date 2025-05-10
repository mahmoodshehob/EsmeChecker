using EsmeChecker.Entities;

namespace EsmeChecker.DataAccess.Repository.IRepository
{
	public interface IMaxMinConfigRepository : IRepository<MaxMinConfig>
	{
		Task<MaxMinConfig> GetMaxMinConfig();
	}
}
