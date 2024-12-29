#region Before building web app
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();  //need to register CARTER to asp.net core DI

builder.Services.AddMediatR(config =>  //tell the program mediatr where to get the handler
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

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
