using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Entities;
using EsmeChecker.Entities.Dtos;
using EsmeChecker.Models.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text;

namespace EsmeChecker.WebAPI.Controllers
{
	public class EmploweeController : ControllerBase
	{
		private readonly IUnitOfServices unitOfServices;

		public EmploweeController(IUnitOfServices unitOfServices)
		{
			this.unitOfServices = unitOfServices;
		}




		[HttpGet]
		[Route("api/[Controller]/GetAll")]
		public async Task<IActionResult> GetAll(PaginatedModel paginatedModel)
		{
			var result = await unitOfServices.EmploweeServcie.GetAll(paginatedModel);
			return StatusCode((int)result.StatusCode, result);
		}

		[HttpGet]
		[Route("api/[Controller]/View")]
		public async Task<IActionResult> View(int emploweeId)
		{
			var result = await unitOfServices.EmploweeServcie.View(emploweeId);
			return StatusCode((int)result.StatusCode, result);
		}

		[HttpPost]
		[Route("api/[Controller]/Create")]
		public async Task<IActionResult> Create(EmploweeDto emplowee)
		{
			var result = await unitOfServices.EmploweeServcie.Create(emplowee);
			return StatusCode((int)result.StatusCode, result);
		}

		[HttpPut]
		[Route("api/[Controller]/Update")]
		public async Task<IActionResult> Update(EmploweeDto emplowee)
		{
			var result = await unitOfServices.EmploweeServcie.Update(emplowee);
			return StatusCode((int)result.StatusCode, result);
		}
	}
}
