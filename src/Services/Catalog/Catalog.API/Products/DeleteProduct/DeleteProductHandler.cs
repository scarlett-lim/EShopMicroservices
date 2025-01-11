
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductByIdCommand(Guid Id) : ICommand<DeleteProductByIdResult>;

    public record DeleteProductByIdResult(bool IsSuccess);

    internal class DeleteProductByIdCommandHandler(IDocumentSession session, ILogger<DeleteProductByIdCommandHandler> logger)
        : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
    {
        public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);

            //Pending Changes
            session.Delete<Product>(command.Id);

            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductByIdResult(true);
        }

    }
}
