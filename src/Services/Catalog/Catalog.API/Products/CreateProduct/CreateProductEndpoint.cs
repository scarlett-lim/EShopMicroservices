namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // minimal api post method
            app.MapPost("/products",
                async (CreateProductRequest request, ISender sender) =>
            {
                // carter help to "serialize" the incoming request to createproductcommand, smthg like json serialization
                var command = request.Adapt<CreateProductCommand>();

                // and send it using mediatr, it will then trigger the mediatr handle class
                var result = await sender.Send(command);

                // carter help to "serialize" the returned result to createproductresponse
                var response = result.Adapt<CreateProductResponse>();

                //return 201 and guid to client application if it is success
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")  //endpoint name, can be used to generate the url
            .Produces<CreateProductResponse>(StatusCodes.Status201Created) //help api tools like Swagger/OpenAPI/Carter to generate API DOC
            .ProducesProblem(StatusCodes.Status400BadRequest) //help api tools like Swagger/OpenAPI/Carter to generate API DOC//return 400 if there is error
            .WithSummary("Create Product")
            .WithDescription("Create Product");


        }
    }
}
