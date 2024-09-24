using Demo.Products.Common;
using Demo.Products.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Demo.Products.CQRS.Products
{
    public record DeleteProductCommand(Guid Id) : IRequest<OneOf<NotFound, Success, Failed>>
    {
        internal class DeleteProductCommandHandler(ProductsDb productsDb) : IRequestHandler<DeleteProductCommand, OneOf<NotFound, Success, Failed>>
        {
            public async Task<OneOf<NotFound, Success, Failed>> Handle(DeleteProductCommand request, CancellationToken ct)
            {
                try
                {
                    var rows = await productsDb.Products.Where(x => x.Id == request.Id).ExecuteDeleteAsync(ct);

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
