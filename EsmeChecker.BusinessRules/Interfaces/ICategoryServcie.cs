using EsmeChecker.Entities;
using EsmeChecker.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Interfaces
{
    public interface ICategoryServcie
	{
		Task<IEnumerable<Category>> GetAll(PaginatedModel paginated);
	}
}
