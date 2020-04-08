using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Dysnomia.WhoOwnsMe.WebApp.Controllers {
	public class UrlRequestCultureProvider : IRequestCultureProvider {
		public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext) {
			var url = httpContext.Request.Path;

			var parts = url.Value
				.Split('/')
				.Where(p => !String.IsNullOrWhiteSpace(p)).ToList();
			if (parts.Count == 0) {
				return Task.FromResult<ProviderCultureResult>(null);
			}

			var cultureSegmentIndex = 0;
			var hasCulture = Regex.IsMatch(
				parts[cultureSegmentIndex],
				@"^[a-z]{2}(?:-[A-Z]{2})?$");
			if (!hasCulture) {
				return Task.FromResult<ProviderCultureResult>(null);
			}

			var culture = parts[cultureSegmentIndex];
			return Task.FromResult(new ProviderCultureResult(culture));
		}
	}
}
