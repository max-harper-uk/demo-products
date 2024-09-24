using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using OneOf;

namespace Demo.Products.CQRS.ProductOptions
{
    public record CreateProductOptionCommand(Guid ProductId, ProductOption ProductOption) : IRequest<OneOf<Success<ProductOption>, Failed>>
    {
        internal sealed class CreateProductOptionCommandHandler(ProductsDb dbContext) : IRequestHandler<CreateProductOptionCommand, OneOf<Success<ProductOption>, Failed>>
        {
            public async Task<OneOf<Success<ProductOption>, Failed>> Handle(CreateProductOptionCommand command, CancellationToken ct)
            {
                command.ProductOption.Id = Guid.NewGuid();
                command.ProductOption.ProductId = command.ProductId;

                try
                {
                    await dbContext.AddAsync(command.ProductOption, ct);
                    await dbContext.SaveChangesAsync(ct);

                    return new Success<ProductOption>(command.ProductOption);
                }
                catch (Exception ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
