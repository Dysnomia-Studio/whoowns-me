using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dysnomia.Common.Security;
using Dysnomia.WhoOwnsMe.Business.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Dysnomia.WhoOwnsMe.WebApp.Controllers {
	public class HomeController : Controller {
		private readonly IPropertyService propertyService;
		public HomeController(IPropertyService propertyService) {
			this.propertyService = propertyService;
		}

		[HttpGet("/")]
		public async Task<IActionResult> Index() {
			BotHelper.SetSessionsVars(HttpContext);

			ViewData["TopItems"] = await propertyService.GetTopProperties();

			return View();
		}

		[HttpGet("/search/{searchText}")]
		public async Task<IActionResult> Search(string searchText) {
			IEnumerable<string> result = null;
			ViewData["SearchText"] = searchText;
			if (!BotHelper.IsBot(HttpContext) && !string.IsNullOrWhiteSpace(searchText)) {
				result = await propertyService.Search(searchText);
			}

			BotHelper.SetSessionsVars(HttpContext);

			if (result == null) {
				return View("Index");
			}

			if (!result.Any()) {
				ViewData["error"] = "Erreur: Aucun resultat";
				return View("Index");
			}

			ViewData["Results"] = result;

			return View();
		}

		[HttpGet("/info/{name}")]
		public async Task<IActionResult> Info(string name) {
			var result = await propertyService.GetPropertyByName(name);
			ViewData["Result"] = result;

			if (ViewData["Result"] == null) {
				ViewData["error"] = "Erreur: Page inexistante";
				return View("Index");
			}

			await propertyService.AddViewToItem(result.Name);

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View();
		}
	}
}
