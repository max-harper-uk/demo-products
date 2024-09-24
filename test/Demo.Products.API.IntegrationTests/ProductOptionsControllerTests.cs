using AutoFixture.Xunit2;
using Demo.Products.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace Demo.Products.API.IntegrationTests
{
    public class ProductOptionsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProductOptionsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_ProductOptions_Endpoint_Returns_200_List_Of_ProductOptions()
        {
            // Arrange
            const string productId = "8f2e9176-35ee-4f0a-ae55-83023d2db1a3";

            // Act
            var response = await _client.GetAsync($"/products/{productId}/options");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<List<ProductOption>>(await response.Content.ReadAsStringAsync());

            body.ShouldNotBeNull();
            body.Count.ShouldBe(2);

            body.ShouldContain(x =>
                x.Id == new Guid("0643ccf0-ab00-4862-b3c5-40e2731abcc9") &&
                x.ProductId == new Guid(productId) &&
                x.Name == "White" &&
                x.Description == "White Samsung Galaxy S7");

            body.ShouldContain(x =>
                x.Id == new Guid("a21d5777-a655-4020-b431-624bb331e9a2") &&
                x.ProductId == new Guid(productId) &&
                x.Name == "Black" &&
                x.Description == "Black Samsung Galaxy S7");
        }

        [Theory]
        [AutoData]
        public async Task Create_ProductOption_Endpoint_Returns_201_ProductOption(ProductOption newOption)
        {
            // Arrange
            const string ProductId = "de1287c0-4b15-4a7b-9d8a-dd21b3cafec3";

            // Act
            var response = await _client.PostAsJsonAsync($"/products/{ProductId}/options", newOption);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            var body = JsonConvert.DeserializeObject<ProductOption>(await response.Content.ReadAsStringAsync());

            body.ShouldNotBeNull();
            body.Id.ShouldBeAssignableTo<Guid>();
            body.ProductId.ShouldBe(new Guid(ProductId));
            body.Name.ShouldBe(newOption.Name);
            body.Description.ShouldBe(newOption.Description);

            response.Headers.ShouldContain(
                x => x.Key == "Location" &&
                x.Value.Contains($"http://localhost/products/{ProductId}/options/{body.Id}"));
        }
    }
}