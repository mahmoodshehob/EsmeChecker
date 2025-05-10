using EsmeChecker.BusinessRules.Interfaces;
using EsmeChecker.BusinessRules.Services;
using EsmeChecker.Models.Sybase;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EsmeChecker.WebAPI.Controllers
{
    public class KannelController : ControllerBase
	{

		private readonly IUnitOfServices unitOfServices;

		public KannelController(IUnitOfServices unitOfServices)
		{
			this.unitOfServices = unitOfServices;
		}

		[HttpGet()]
		[Route("api/[Controller]/Status")]
		public async Task<ActionResult> Status()
		{
			return StatusCode(200, await unitOfServices.KannelService.Status());
		}

		//[HttpGet("status")]
		//public async Task<IActionResult> GetStatus()
		//{
		//	var status = await _statusService.GetKannelStatus();
		//	if (status == null) return NotFound("Status file not found.");
		//	return Ok(status);
		//}


		[HttpPost]
		[Route("api/[Controller]/SendSMS")]
		public async Task<ActionResult> SendSMS(string targetNumber, string messageContent)
		{
			await unitOfServices.KannelService.SendSMS(targetNumber, messageContent);
			return StatusCode(200);
		}
	}
}
