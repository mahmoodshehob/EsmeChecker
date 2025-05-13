using Azure;
using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;
using EsmeChecker.Entities.Dtos;
using EsmeChecker.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace EsmeChecker.BusinessRules.Services
{
	public class EmploweeServcie : IEmploweeServcie
	{
		private readonly IUnitOfWork unitOfWork;

		public EmploweeServcie(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task<ServiceResponse<IEnumerable<Emplowee>>> GetAll(PaginatedModel paginated)
		{
			try
			{
				if (paginated.Value == null || String.IsNullOrEmpty(paginated.Value.Replace(" ", "")))
				{
					var result = await unitOfWork.Emplowee.GetAll(p=>p.Id== paginated.Id || p.Name.Contains(paginated.Value), PageSize: paginated.PageSize, PageNumber: paginated.PageNumber);

					var response = ServiceResponse<IEnumerable<Emplowee>>.Success(result);
					
					return response;
				}
				else
				{
					var result = await unitOfWork.Emplowee.GetAll(f => f.Msisdn.Contains(paginated.Value), PageSize: paginated.PageSize, PageNumber: paginated.PageNumber);

					var response = ServiceResponse<IEnumerable<Emplowee>>.Success(result);
					
					return response;
				}
			}
			catch (Exception ex) 
			{
				var response= ServiceResponse<IEnumerable<Emplowee>>.Failure(message: ex.Message, details: ex.InnerException.ToString());
				
				return response;
			}
		}

		public async Task<ServiceResponse<Emplowee>> View(int EmploweeId)
		{
			try
			{
				var result = await unitOfWork.Emplowee.Get(u => u.Id == EmploweeId);


				var response = ServiceResponse<Emplowee>.Success(result);

				return response;
			}
			catch (Exception ex)
			{
				var response = ServiceResponse<Emplowee>.Failure(message: ex.Message, details: ex.InnerException.ToString());

				return response;
			}
		}

		public async Task<ServiceResponse<Boolean>> Create(EmploweeDto emplowee)
		{
			Emplowee newEmplowee = new()
			{
				Msisdn = emplowee.Msisdn,
				Name = emplowee.Name,
				Email = emplowee.Email,
				Postion = emplowee.Postion,
				Allow = true,
				CategoryId = 2,
				CreateDate = DateTime.UtcNow,
			};

			try
			{
				await unitOfWork.Emplowee.Add(newEmplowee);


				var response = ServiceResponse<Boolean>.Success(true);

				return response;
			}
			catch (Exception ex)
			{
				var response = ServiceResponse<Boolean>.Failure(message: ex.Message, details: ex.InnerException.ToString());

				return response;
			}
		}

		public async Task<ServiceResponse<Boolean>> Update(EmploweeDto emplowee)
		{
			var check = await View(emplowee.Id);

			var oldEmplowee = check.Response;

			if (check != null)
			{
				Emplowee newEmplowee = new()
				{
					Id = emplowee.Id,
					Msisdn = emplowee.Msisdn,
					Name = emplowee.Name,
					Email = emplowee.Email,
					Postion = emplowee.Postion,
					Allow = emplowee.Allow,
					CategoryId = emplowee.CategoryId,
					CreateDate = oldEmplowee.CreateDate,
					ModifyDate = DateTime.UtcNow,
				};
			
			try
			{
				await unitOfWork.Emplowee.Add(newEmplowee);


				var response = ServiceResponse<Boolean>.Success(true);

				return response;
			}
			catch (Exception ex)
			{
				var response = ServiceResponse<Boolean>.Failure(message: ex.Message, details: ex.InnerException.ToString());

				return response;
			}}

			return ServiceResponse<Boolean>.Failure();
		}

	}
}
