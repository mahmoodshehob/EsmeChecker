using EsmeChecker.Entities;

namespace EsmeChecker.DataAccess.Repository.IRepository

{
	public interface IEmploweeRepository : IRepository<Emplowee>
	{
		Task Update(Emplowee emplowee);
	}
}
