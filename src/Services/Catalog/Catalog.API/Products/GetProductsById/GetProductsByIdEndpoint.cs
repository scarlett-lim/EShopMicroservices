using Catalog.API.Products.GetProduct;

namespace Catalog.API.Products.GetProductsById
{
    // used this if there is json object or any body passed in
    // if the param is in the endpoint then no nid use this
    //public record GetProductsByIdRequest(Guid Id);

    //the param naming here must same as the handler class as the mapster will use it for orm
    public record GetProductsByIdResponse(Product Product);

    public class GetProductsByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // passed in the param without param name, just the value
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                // passed in the query object to getproductsbyidhandler
                var result = await sender.Send(new GetProductsByIdQuery(id));

                // map the result to GetProductByIdResponse
                var response = result.Adapt<GetProductsByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductsById")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Id")
            .WithDescription("Get Products By Id");
        }
    }
}
