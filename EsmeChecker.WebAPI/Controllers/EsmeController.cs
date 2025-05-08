using EsmeChecker.Models.Sybase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EsmeChecker.BusinessRules.Interfaces;
using System.Threading.Tasks;

namespace EsmeChecker.WebAPI.Controllers
{

	[ApiController]
	[Route("api/[Controller]/")]
	public class EsmeController : ControllerBase
	{
		IEsmeDbServices _esmeDbServices;

		public EsmeController (IEsmeDbServices esmeDbServices)
		{
			_esmeDbServices = esmeDbServices;
		
		}

		// GET: EsmesController
		[HttpGet()]
		[Route("[Action]")]
		public async Task<IActionResult> Index()
		{
			return Ok(await _esmeDbServices.QueryAllEsme());
		}

		[HttpGet()]
		[Route("[Action]")]
		public async Task<ActionResult> MaxMin()
		{

			List<Esme> esme = await _esmeDbServices.MaxMin();


			return Ok(esme);
		}

		//// GET: EsmesController/Details/5
		//public ActionResult Details(int id)
		//{
		//    return View();
		//}

		//// GET: EsmesController/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		//// POST: EsmesController/Create
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create(IFormCollection collection)
		//{
		//    try
		//    {
		//        return RedirectToAction(nameof(Index));
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		//// GET: EsmesController/Edit/5
		//public ActionResult Edit(int id)
		//{
		//    return View();
		//}

		//// POST: EsmesController/Edit/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit(int id, IFormCollection collection)
		//{
		//    try
		//    {
		//        return RedirectToAction(nameof(Index));
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}

		//// GET: EsmesController/Delete/5
		//public ActionResult Delete(int id)
		//{
		//    return View();
		//}

		//// POST: EsmesController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete(int id, IFormCollection collection)
		//{
		//    try
		//    {
		//        return RedirectToAction(nameof(Index));
		//    }
		//    catch
		//    {
		//        return View();
		//    }
		//}
	}
}
