using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
		//ICategoryRepository Category { get; }
		//IProductRepository Product { get; }
		//void Save();

		ISybaseRepository SybaseRepository { get; }

    }
}
