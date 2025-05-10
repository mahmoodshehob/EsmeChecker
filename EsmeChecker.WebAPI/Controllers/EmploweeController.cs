using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Repository.IRepository;
using EsmeChecker.Models.Helper;
using Microsoft.AspNetCore.Mvc;
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
			return Ok(await unitOfServices.EmploweeServcie.GetAll(paginatedModel));
		}


		//[HttpPost]
		//[Route("api/[Controller]/Create")]
		//public async Task<ContentResult> Create(string ussdServiceCode, string mSISDN, string ussdRequestString)
		//{
		//	using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
		//	{
		//		ContentResult response = new ContentResult();

		//		return await unitOfServices.MainServices.QueryEsmeServiceDirect(ussdServiceCode, mSISDN, ussdRequestString);
		//	}
		//}

	}
}
