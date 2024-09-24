using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using OneOf;

namespace Demo.Products.CQRS.Products
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<OneOf<NotFound, Success<Product>, Failed>>
    {
        internal sealed class GetProductByIdQueryHandler(ProductsDb dbContext) : IRequestHandler<GetProductByIdQuery, OneOf<NotFound, Success<Product>, Failed>>
        {
            public async Task<OneOf<NotFound, Success<Product>, Failed>> Handle(GetProductByIdQuery request, CancellationToken ct)
            {
                try
                {
                    var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == request.ProductId, ct);

                    if (product == null)
                    {
                        return new NotFound();
                    }

                    return new Success<Product>(product);
                }
                catch (MySqlException ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
