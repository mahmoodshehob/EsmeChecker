using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
		ISybaseRepository SybaseRepository { get; }
		ICategoryRepository Category { get; }
		IEmploweeRepository Emplowee { get; }
		IMaxMinConfigRepository MaxMinConfig { get; }
		

		void Save();
	}
}
