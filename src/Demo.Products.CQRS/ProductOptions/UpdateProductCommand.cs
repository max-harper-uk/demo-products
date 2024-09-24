using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Demo.Products.CQRS.ProductOptions
{
    public record UpdateProductOptionCommand(Guid Id, ProductOption Product) : IRequest<OneOf<NotFound, Success<ProductOption>, Failed>>
    {
        internal class UpdateProductOptionCommandHandler(ProductsDb productsDb) : IRequestHandler<UpdateProductOptionCommand, OneOf<NotFound, Success<ProductOption>, Failed>>
        {
            public async Task<OneOf<NotFound, Success<ProductOption>, Failed>> Handle(UpdateProductOptionCommand request, CancellationToken ct)
            {
                var existing = await productsDb.ProductOptions.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

                if (existing == null)
                {
                    return new NotFound();
                }

                /* Implement the invalid return type along with a model validation layer
                 * instead of relying on invalid requests to fail at the DB level.
                  
                if(existing.ProductId != request.Product.ProductId)
                {
                    return new Invalid();
                }
                */

                existing.Name = request.Product.Name;
                existing.Description = request.Product.Description;

                try
                {
                    await productsDb.SaveChangesAsync(ct);
                }
                catch (Exception ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }

                return new Success<ProductOption>();
            }
        }
    }
}
