namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string UserName);

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule //ICarterModule : For Minimal APIs
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //In delegate handler param. the first param(string username) populate from the request params
        //2nd param will be injected by ASP.NET built in Dependency Injection
        app.MapGet("/basket/{userName}", async (string username, ISender sender) => 
        {
            var result = await sender.Send(new GetBasketQuery(username));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("GetBasketByUserName")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket By UserName")
        .WithDescription("Get Basket By UserName");
    }

}
