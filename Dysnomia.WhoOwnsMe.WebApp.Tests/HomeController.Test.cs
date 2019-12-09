using System.Net;
using System.Net.Http;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Xunit;


namespace Dysnomia.WhoOwnsMe.WebApp.Tests {
	public class HomeController {
		public HttpClient client { get; }
		public TestServer server { get; }

		public HomeController() {
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false)
				.Build();

			var builder = new WebHostBuilder()
				 .UseConfiguration(config)
				.UseStartup<Startup>()
				.UseEnvironment("Testing");
			var server = new TestServer(builder);

			client = server.CreateClient();
		}

		[Fact]
		public async void ShouldGet200_GET_Index() {
			var response = await client.GetAsync("/");

			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}
	}
}
