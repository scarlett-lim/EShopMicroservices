#region Before building web app
using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>  //tell the program mediatr where to get the handler
{
    config.RegisterServicesFromAssembly(assembly);

    //add validation behavior as a pipeline behavior into mediatr
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();  //need to register CARTER to asp.net core DI

builder.Services.AddMarten(opts =>  
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();
#endregion


#region After building web app

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
#endregion
