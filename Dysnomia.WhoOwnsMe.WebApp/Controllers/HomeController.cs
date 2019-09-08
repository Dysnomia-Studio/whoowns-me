using Microsoft.AspNetCore.Mvc;

namespace Dysnomia.WhoOwnsMe.WebApp.Controllers {
	public class HomeController:Controller {
		public IActionResult Index() {
			return View();
		}

		public IActionResult Search(string s) {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View();
		}
	}
}
