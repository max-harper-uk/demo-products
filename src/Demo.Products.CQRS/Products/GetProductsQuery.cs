using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using OneOf;


namespace Demo.Products.CQRS.Products
{
    public record GetProductsQuery(int Skip = 0, int Take = 100, string? Name = null) : IRequest<OneOf<Success<PagedList<Product>>, Failed>>
    {
        internal sealed class GetProductsQueryHandler(ProductsDb dbContext) : IRequestHandler<GetProductsQuery, OneOf<Success<PagedList<Product>>, Failed>>
        {
            public async Task<OneOf<Success<PagedList<Product>>, Failed>> Handle(GetProductsQuery request, CancellationToken ct)
            {
                try
                {
                    IQueryable<Product> query = dbContext.Products.OrderBy(x => x.Name);

                    if (request.Name != null)
                    {
                        var nameParam = request.Name.ToLowerInvariant();
                        query = query.Where(x => x.Name.Contains(nameParam));
                    }

                    var resultCount = await query.CountAsync(ct);

                    List<Product> results;

                    if (resultCount > 0)
                    {
                        results = await query
                        .Skip(request.Skip)
                        .Take(request.Take)
                        .ToListAsync(ct);
                    }
                    else
                    {
                        results = new List<Product>();
                    }

                    var result = new PagedList<Product>(results, request.Skip, request.Take, resultCount);

                    return new Success<PagedList<Product>>(result);
                }
                catch (MySqlException ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
