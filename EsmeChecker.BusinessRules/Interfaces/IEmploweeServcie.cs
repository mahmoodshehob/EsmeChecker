using EsmeChecker.Entities;
using EsmeChecker.Entities.Dtos;
using EsmeChecker.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsmeChecker.BusinessRules.Interfaces
{
    public interface IEmploweeServcie
    {
		Task<ServiceResponse<IEnumerable<Emplowee>>> GetAll(PaginatedModel paginated);

		Task<ServiceResponse<Emplowee>> View(int EmploweeId);

		Task<ServiceResponse<Boolean>> Create(EmploweeDto emplowee);

		Task<ServiceResponse<Boolean>> Update(EmploweeDto emplowee);
	}
}
