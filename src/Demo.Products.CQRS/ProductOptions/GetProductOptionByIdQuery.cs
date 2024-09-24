using Demo.Products.Common;
using Demo.Products.Database;
using Demo.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using OneOf;

namespace Demo.Products.CQRS.ProductOptions
{
    public record GetProductOptionByIdQuery(Guid Id) : IRequest<OneOf<NotFound, Success<ProductOption>, Failed>>
    {
        internal sealed class GetProductOptionByIdQueryHandler(ProductsDb dbContext) : IRequestHandler<GetProductOptionByIdQuery, OneOf<NotFound, Success<ProductOption>, Failed>>
        {
            public async Task<OneOf<NotFound, Success<ProductOption>, Failed>> Handle(GetProductOptionByIdQuery request, CancellationToken ct)
            {
                try
                {
                    var option = await dbContext.ProductOptions.SingleOrDefaultAsync(x => x.Id == request.Id, ct);

                    if (option == null)
                    {
                        return new NotFound();
                    }

                    return new Success<ProductOption>(option);
                }
                catch (MySqlException ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
