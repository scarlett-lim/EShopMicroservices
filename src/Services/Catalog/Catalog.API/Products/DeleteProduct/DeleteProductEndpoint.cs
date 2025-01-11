
using Catalog.API.Products.GetProductsByCategory;

namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductByIdRequest(Guid Id);

    public record DeleteProductByIdResponse(bool IsSuccess);

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapDelete("/product/{id}",
               async(Guid id, ISender sender) =>
               {
                   var result = await sender.Send(new DeleteProductByIdCommand(id));

                   var response = result.Adapt<DeleteProductByIdResponse>();

                   return Results.Ok(response);
               })
                .WithName("DeleteProductById")
                .Produces<GetProductsByCategoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product By Id")
                .WithDescription("Delete Product By Id");
        }
    }
}
