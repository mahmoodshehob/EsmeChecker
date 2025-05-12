using EsmeChecker.BusinessRules.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EsmeChecker.WebAPI.Controllers
{
    public class ConfigrationController : ControllerBase
    {
        private readonly IUnitOfServices unitOfServices;

        public ConfigrationController(IUnitOfServices unitOfServices)
        {
            this.unitOfServices = unitOfServices;
        }



        [HttpGet()]
        [Route("[Action]")]
        public async Task<ActionResult> GetConfigration()
        {
            var result = await unitOfServices.MaxMinConfigService.GetConfig();

			return Ok(result);
        }
    }
}
