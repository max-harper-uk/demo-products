using Demo.Products.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xero.Products.API.Controllers.BaseController;
using Demo.Products.Common;
using Demo.Products.CQRS.Products;

namespace Demo.Products.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ExtendedControllerBase<ProductsController>
    {
        public ProductsController(ISender sender, ILogger<ProductsController> logger) : base(logger, sender) { }

        [HttpGet(Actions.GetProducts.Path, Name = Actions.GetProducts.Name)]
        [Produces<PagedList<Product>>]
        public async Task<IActionResult> GetAsync(int skip = 0, int take = 100, string? name = null)
        {
            var result = await _sender.Send(new GetProductsQuery(skip, take, name));

            return result.Match<IActionResult>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Error));
        }

        [HttpGet(Actions.GetProductById.Path, Name = Actions.GetProductById.Name)]
        [Produces<Product>]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id));

            return result.Match<IActionResult>(
                notFound => NotFound(),
                success => Ok(success.Value),
                failed => BadRequest(failed.Error));
        }

        [HttpPost(Actions.CreateProduct.Path, Name = Actions.CreateProduct.Name)]
        [Produces<Product>]
        public async Task<IActionResult> PostAsync(Product product)
        {
            var result = await _sender.Send(new CreateProductCommand(product));

            return result.Match<IActionResult>(
                success => CreatedAtRoute(
                    Actions.GetProductById.Name, 
                    new { id = product.Id }, 
                    product),
                failed => UnprocessableEntity(failed));
        }

        [HttpPut(Actions.UpdateProduct.Path, Name = Actions.UpdateProduct.Name)]
        [Produces<Product>]
        public async Task<IActionResult> PutAsync(Guid id, Product product)
        {
            var result = await _sender.Send(new UpdateProductCommand(id, product));

            return result.Match<IActionResult>(
                notFound => NotFound(),
                success => Ok(success.Value),
                failed => UnprocessableEntity(failed));
        }

        [HttpDelete(Actions.DeleteProduct.Path, Name = Actions.DeleteProduct.Name)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _sender.Send(new DeleteProductCommand(id));

            return result.Match<IActionResult>(
                notFound => NotFound(),
                success => NoContent(),
                failed => UnprocessableEntity());
        }


        /// <summary>
        /// Action names and routes for this controller, use these to generate HAL resource links
        /// </summary>
        public static class Actions
        {
            public static class GetProducts
            {
                public const string Name = nameof(GetProducts);
                public const string Path = "products";
            };
            public static class GetProductById
            {
                public const string Name = nameof(GetProductById);
                public const string Path = "products/{id}";
            }
            public static class CreateProduct
            {
                public const string Name = nameof(CreateProduct);
                public const string Path = "products";
            }
            public static class UpdateProduct
            {
                public const string Name = nameof(UpdateProduct);
                public const string Path = "products/{id}";
            }
            public static class DeleteProduct
            {
                public const string Name = nameof(DeleteProduct);
                public const string Path = "products/{id}";
            }
        }
    }
}
