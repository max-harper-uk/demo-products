using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Demo.Products.CQRS.Products
{
    public record UpdateProductCommand(Guid Id, Product Product) : IRequest<OneOf<NotFound, Success<Product>, Failed>>
    {
        internal class UpdateProductCommandHandler(ProductsDb productsDb) : IRequestHandler<UpdateProductCommand, OneOf<NotFound, Success<Product>, Failed>>
        {
            public async Task<OneOf<NotFound, Success<Product>, Failed>> Handle(UpdateProductCommand request, CancellationToken ct)
            {
                var existing = await productsDb.Products.FirstOrDefaultAsync(x => x.Id == request.Id, ct);

                if (existing == null)
                {
                    return new NotFound();
                }

                existing.Name = request.Product.Name;
                existing.Description = request.Product.Description;
                existing.Price = request.Product.Price;
                existing.DeliveryPrice = request.Product.DeliveryPrice;

                try
                {
                    await productsDb.SaveChangesAsync(ct);
                }
                catch (Exception ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }

                return new Success<Product>();
            }
        }
    }
}
