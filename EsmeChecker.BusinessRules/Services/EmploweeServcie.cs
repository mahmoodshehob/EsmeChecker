using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;
using EsmeChecker.Models.Helper;
using Microsoft.Extensions.Configuration;

namespace EsmeChecker.BusinessRules.Services
{
    public class EmploweeServcie : IEmploweeServcie
	{
		private readonly IUnitOfWork unitOfWork;

		public EmploweeServcie(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Emplowee>> GetAll(PaginatedModel paginated)
		{
			if (paginated.Value == null || String.IsNullOrEmpty(paginated.Value.Replace(" ", "")))
			{
				return await unitOfWork.Emplowee.GetAll(PageSize: paginated.PageSize, PageNumber: paginated.PageNumber);
			}
			else
			{
				return await unitOfWork.Emplowee.GetAll(f => f.Msisdn.Contains(paginated.Value), PageSize: paginated.PageSize, PageNumber: paginated.PageNumber);
			}
		}
	}
}
