﻿
namespace Catalog.API.Products.GetProduct
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<Product> Products);


    internal class GetProductsQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                //if pagenumber is not provided, default to 1, same concept applies to pagesize
                .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
