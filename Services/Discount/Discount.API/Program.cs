using Common.Logging;
using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositories;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//SeriLog Configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),                     // Usually the API project
    typeof(CreateDiscountCommandHandler).Assembly,               // Catalog.Application
};

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(assemblies);});

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddGrpc();

var app = builder.Build();

//Migrate the database
app.MigrateDatabase<Program>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{ 
    endpoints.MapGrpcService<DiscountService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a grpc client");
    });
});

app.Run();

