using System.Threading.Tasks;

using Dysnomia.WhoOwnsMe.Business.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Dysnomia.WhoOwnsMe.WebApp.Controllers {
	public class HomeController : Controller {
		public IPropertyService propertyService;
		public HomeController(IPropertyService propertyService) {
			this.propertyService = propertyService;
		}

		[HttpGet("/")]
		public IActionResult Index() {
			return View();
		}

		[HttpGet("/search/{s}")]
		public async Task<IActionResult> Search(string s) {
			return View(await propertyService.Search(s));
		}

		[HttpGet("/info/{name}")]
		public async Task<IActionResult> Info(string name) {
			var obj = await propertyService.GetPropertyByName(name);

			if (obj == null) {
				return NotFound();
			}

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View();
		}
	}
}
