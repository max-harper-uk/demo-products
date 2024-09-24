using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using OneOf;

namespace Demo.Products.CQRS.Products
{
    public record CreateProductCommand(Product Product) : IRequest<OneOf<Success<Product>, Failed>>
    {
        internal sealed class CreateProductCommandHandler(ProductsDb dbContext) : IRequestHandler<CreateProductCommand, OneOf<Success<Product>, Failed>>
        {
            public async Task<OneOf<Success<Product>, Failed>> Handle(CreateProductCommand command, CancellationToken ct)
            {
                command.Product.Id = Guid.NewGuid();

                try
                {
                    await dbContext.AddAsync(command.Product, ct);
                    await dbContext.SaveChangesAsync(ct);

                    return new Success<Product>(command.Product);
                }
                catch (Exception ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
