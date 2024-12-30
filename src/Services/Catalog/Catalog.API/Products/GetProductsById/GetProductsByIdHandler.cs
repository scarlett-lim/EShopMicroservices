namespace Catalog.API.Products.GetProductsById
{
    public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;
    public record GetProductsByIdResult(Product Product);

    internal class GetProductsByIdQueryHandler(IDocumentSession session, ILogger<GetProductsByIdQueryHandler> logger)
        : IQueryHandler<GetProductsByIdQuery, GetProductsByIdResult>
    {
        public async Task<GetProductsByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByIdQueryHandler.Handle called with {@Query}", query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            return product is null ? throw new ProductNotFoundException() : new GetProductsByIdResult(product);
        }
    }
}
