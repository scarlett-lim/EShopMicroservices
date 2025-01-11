
using Catalog.API.Products.GetProductsByCategory;

namespace Catalog.API.Products.UpdateProduct
{
    //the param name must for request must same as the param for command for Mapster to map to correct entity
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsSuccess);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products",
                async (UpdateProductRequest request, ISender sender) =>
                {
                    var command = request.Adapt<UpdateProductCommand>();

                    var result = await sender.Send(command);

                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                })
                .WithName("UpdateProduct")
                .Produces<GetProductsByCategoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product"); 
        }
    }
}
