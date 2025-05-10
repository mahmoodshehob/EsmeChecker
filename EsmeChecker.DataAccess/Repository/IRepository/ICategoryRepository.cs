using EsmeChecker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		Task Update(Category category);
	}
}
