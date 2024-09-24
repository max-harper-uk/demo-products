using Demo.Products.Common;
using Demo.Products.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Demo.Products.CQRS.ProductOptions
{
    public record DeleteProductOptionCommand(Guid Id) : IRequest<OneOf<NotFound, Success, Failed>>
    {
        internal class DeleteProductOptionCommandHandler(ProductsDb productsDb) : IRequestHandler<DeleteProductOptionCommand, OneOf<NotFound, Success, Failed>>
        {
            public async Task<OneOf<NotFound, Success, Failed>> Handle(DeleteProductOptionCommand request, CancellationToken ct)
            {
                try
                {
                    var rows = await productsDb.ProductOptions.Where(x => x.Id == request.Id).ExecuteDeleteAsync(ct);

                    if (rows == 0) return new NotFound();

                    return new Success();
                }
                catch (Exception ex)
                {
                    return new Failed { Error = "Database Exception", Exception = ex };
                }
            }
        }
    }
}
