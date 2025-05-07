using EsmeChecker.BusinessRules.Helper;
using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.DataAccess.Data;
using EsmeChecker.Models.Ussd;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EsmeChecker.WebAPI.Controllers
{

	[ApiController]
	public class UssdApiController : ControllerBase
	{
		private const string contentType = "text/xml";
		
		private readonly IUnitOfServices unitOfServices;

		public UssdApiController(IUnitOfServices unitOfServices)
		{
			this.unitOfServices = unitOfServices;
		}

		[HttpPost]
		[Consumes(contentType)]
		[Route("api/[Controller]/Query")]
		public async Task<ContentResult> Post()
		{
			using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
				ContentResult response = new ContentResult();

				string xmlContent = await reader.ReadToEndAsync();

				return await unitOfServices.MainServices.QueryEsmeService(xmlContent);
			}
		}
	}
}