using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using OneOf;


namespace Demo.Products.CQRS.ProductOptions
{
    public record GetProductOptionsQuery(Guid ProductId) : IRequest<OneOf<Success<List<ProductOption>>, Failed>>
    {
        internal sealed class GetProductsQueryHandler(ProductsDb dbContext) : IRequestHandler<GetProductOptionsQuery, OneOf<Success<List<ProductOption>>, Failed>>
        {
            public async Task<OneOf<Success<List<ProductOption>>, Failed>> Handle(GetProductOptionsQuery request, CancellationToken ct)
            {
                try
                {
                    var results = await dbContext.ProductOptions.Where(x => x.ProductId == request.ProductId)
                        .ToListAsync(ct);

                    return new Success<List<ProductOption>>(results);
                }
                catch (MySqlException ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
