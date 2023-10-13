using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CarsAPIUnitTest
{
    [TestClass]
    public class ModelsControllerTests
    {
        // [Fact]
        private readonly HttpClient _client;

        public ModelsControllerTests()
        {
            // Create an HttpClient for making HTTP requests to your API
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost/"); // Set the base URL of your API
        }

        [Fact]
        public async Task TestApiEndpoint()
        {
            // Arrange - prepare the test, if needed

            // Act - make an HTTP request to your API
            var response = await _client.GetAsync("/api/models/GetModels?Make=55%20GRILLS&Modelyear=2015");

            // Assert - validate the response
            response.EnsureSuccessStatusCode();
            // Add additional assertions as needed
        }
    }
}
