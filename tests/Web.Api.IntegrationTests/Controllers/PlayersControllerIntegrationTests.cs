using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Xunit;

namespace Web.Api.IntegrationTests.Controllers
{
    public class PlayersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    //public class PlayersControllerIntegrationTests 
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public PlayersControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
         
        }

        [Fact]
        public async Task CanGetPlayers()
        {
            Action<IServiceCollection> applyTestServices = services =>
            {
                services.AddScoped<ILoggerService, TestLoggerService>();
            };

            var _client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(applyTestServices);
                })
                .CreateClient();


            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/players");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(stringResponse);
            Assert.Contains(players, p => p.FirstName=="Wayne");
            Assert.Contains(players, p => p.FirstName == "Mario");
        }


        [Fact]
        public async Task CanGetPlayerById()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/players/1");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);
            Assert.Equal(1,player.Id);
            Assert.Equal("Wayne", player.FirstName);
        }
    }
}
