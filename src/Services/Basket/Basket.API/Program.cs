#region Pre application build setting
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();

//Mediatr = for cqrs and direct to correct handler
builder.Services.AddMediatR(config =>
{
    //register mediatr to current assembly
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Marten DI registration
builder.Services.AddMarten(opts =>
{
    //get the connection string from appsettings.json
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    //set UserName as PK
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions(); // use the lightweight session for better performance

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
#endregion


#region Post application build setting
// Configure the HTTP request pipeline
app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
#endregion
