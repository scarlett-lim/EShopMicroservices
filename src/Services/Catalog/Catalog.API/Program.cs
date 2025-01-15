#region Before building web app
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>  //tell the program mediatr where to get the handler
{
    config.RegisterServicesFromAssembly(assembly);
    //add validation behavior as a pipeline behavior into mediatr
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();  //need to register CARTER to asp.net core DI

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();  //initialize the data for development environment
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//add health check for db connection and the catalog.api
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database"));

var app = builder.Build();
#endregion


#region After building web app

// Configure the HTTP request pipeline.
app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    //make the return response as json
    new HealthCheckOptions 
    {
        ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
    });


app.Run();
#endregion
