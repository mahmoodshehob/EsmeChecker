using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;
using EsmeChecker.Models.Helper;
using Microsoft.Extensions.Configuration;

namespace EsmeChecker.BusinessRules.Services
{
    public class CategoryServcie //: ICategoryServcie
	{
		private readonly IUnitOfWork unitOfWork;

		public CategoryServcie(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Category>> GetAll(PaginatedModel paginated)
		{
			if (paginated.Value == null || String.IsNullOrEmpty(paginated.Value.Replace(" ", "")))
			{
				return await unitOfWork.Category.GetAll(PageSize: paginated.PageSize, PageNumber: paginated.PageNumber);
			}
			else
			{
				return await unitOfWork.Category.GetAll(f => 
				f.Administration.Contains(paginated.Value) ||
				f.Department.Contains(paginated.Value) ||
				f.Unit.Contains(paginated.Value)
				, PageSize: paginated.PageSize, PageNumber: paginated.PageNumber);
			}
		}
	}
}
