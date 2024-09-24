using AutoFixture.Xunit2;
using Demo.Products.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using Demo.Products.Common;

namespace Demo.Products.API.IntegrationTests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Products_Endpoint_Returns_200_Paged_List_Of_Products()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/products");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var body = JsonConvert.DeserializeObject<PagedList<Product>>(await response.Content.ReadAsStringAsync());

            body.Items.Count.ShouldBeLessThanOrEqualTo(100);
            body.Page.ShouldBe(1);
            body.PageCount.ShouldBe(1);
            body.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            body.Skip.ShouldBe(0);
            body.Take.ShouldBe(100);

            body.Items.ShouldContain(x =>
                x.Name == "Apple iPhone 6S" &&
                x.Price == 1299.99m &&
                x.DeliveryPrice == 15.99m);

            body.Items.ShouldContain(x =>
                x.Name == "Samsung Galaxy S7" &&
                x.Price == 1024.99m &&
                x.DeliveryPrice == 16.99m);
        }

        [Theory]
        [AutoData]
        public async Task Create_Product_Endpoint_Returns_201_Product(Product newProduct)
        {
            // Act
            var response = await _client.PostAsJsonAsync("/products", newProduct);

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            var body = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            
            body.ShouldNotBeNull();
            body.Id.ShouldBeAssignableTo<Guid>();
            body.Name.ShouldBe(newProduct.Name);
            body.Price.ShouldBe(newProduct.Price);
            body.DeliveryPrice.ShouldBe(newProduct.DeliveryPrice);

            response.Headers.ShouldContain(x => 
                x.Key == "Location" && 
                x.Value.Contains($"http://localhost/products/{body.Id}"));
        }
    }
}