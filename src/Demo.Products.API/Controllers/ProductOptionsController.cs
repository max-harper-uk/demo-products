using Demo.Products.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xero.Products.API.Controllers.BaseController;
using Demo.Products.CQRS.ProductOptions;

namespace Demo.Products.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ProductOptionsController : ExtendedControllerBase<ProductOptionsController>
    {
        public ProductOptionsController(ISender sender, ILogger<ProductOptionsController> logger) : base(logger, sender) { }

        [HttpGet(Actions.GetProductOptions.Path, Name = Actions.GetProductOptions.Name)]
        [Produces<List<ProductOption>>]
        public async Task<IActionResult> GetProductOptionsAsync(Guid productId)
        {
            var result = await _sender.Send(new GetProductOptionsQuery(productId));

            return result.Match<IActionResult>(
                success => Ok(success.Value),
                failed => BadRequest(failed.Error));
        }

        [HttpGet(Actions.GetProductOptionById.Path, Name = Actions.GetProductOptionById.Name)]
        [Produces<ProductOption>]
        public async Task<IActionResult> GetProductOptionByIdAsync(Guid optionId)
        {
            var result = await _sender.Send(new GetProductOptionByIdQuery(optionId));

            return result.Match<IActionResult>(
                notFound => NotFound(),
                success => Ok(success.Value),
                failed => BadRequest(failed.Error));
        }

        [HttpPost(Actions.CreateProductOption.Path, Name = Actions.CreateProductOption.Name)]
        [Produces<ProductOption>]
        public async Task<IActionResult> PostAsync(Guid ProductId, ProductOption productOption)
        {
            var result = await _sender.Send(new CreateProductOptionCommand(ProductId, productOption));

            return result.Match<IActionResult>(
                success => CreatedAtRoute(
                    Actions.GetProductOptionById.Name, 
                    new {productId = success.Value.ProductId, optionId = success.Value.Id}, 
                    success.Value),
                failed => UnprocessableEntity(failed));
        }

        [HttpPut(Actions.UpdateProductOption.Path, Name = Actions.UpdateProductOption.Name)]
        [Produces<ProductOption>]
        public async Task<IActionResult> PutAsync(Guid optionId, ProductOption productOption)
        {
            var result = await _sender.Send(new UpdateProductOptionCommand(optionId, productOption));

            return result.Match<IActionResult>(
                notFound => NotFound(),
                success => Ok(success.Value),
                failed => UnprocessableEntity(failed));
        }

        [HttpDelete(Actions.DeleteProductOption.Path, Name = Actions.DeleteProductOption.Name)]
        public async Task<IActionResult> DeleteAsync(Guid optionId)
        {
            var result = await _sender.Send(new DeleteProductOptionCommand(optionId));

            return result.Match<IActionResult>(
                notFound => NotFound(),
                success => NoContent(),
                failed => UnprocessableEntity());
        }


        /// <summary>
        /// Action names and routes for this controller.
        /// Reference this in HAL resource links and Authorization code
        /// </summary>
        public static class Actions
        {
            public static class GetProductOptions
            {
                public const string Name = nameof(GetProductOptions);
                public const string Path = "products/{productId}/options";
            };
            public static class GetProductOptionById
            {
                public const string Name = nameof(GetProductOptionById);
                public const string Path = "products/{productId}/options/{optionId}";
            }
            public static class CreateProductOption
            {
                public const string Name = nameof(CreateProductOption);
                public const string Path = "products/{productId}/options";
            }
            public static class UpdateProductOption
            {
                public const string Name = nameof(UpdateProductOption);
                public const string Path = "products/{productId}/options/{optionId}";
            }
            public static class DeleteProductOption
            {
                public const string Name = nameof(DeleteProductOption);
                public const string Path = "products/{productId}/{optionId}";
            }
        }
    }
}
