namespace Basket.API.Basket.StoreBasket
{
    // json body must have the same variable name as Cart 
    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndpoints : ICarterModule //ICarterModule : For Minimal APIs
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                // macam json serialization, make the incoming json to .net object
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                // act like json deserialization, make the .net object to json
                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{response.UserName}", response);  
            })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
        }
    }
}
