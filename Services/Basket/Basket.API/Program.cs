using Asp.Versioning;
using Basket.Application.GrpcService;
using Basket.Application.Handlers;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Common.Logging;
using Discount.Grpc.Protos;
using MassTransit;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//SeriLog Configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options => {
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
})

// Add Versioned API explorer
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("ApiVersion", typeof(Asp.Versioning.Routing.ApiVersionRouteConstraint));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { 
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.API", Version = "v1" });
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Basket.API", Version = "v2" });

    // Include xml comments for Swagger documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) {
        c.IncludeXmlComments(xmlPath);
    }

    //Configure Swagger to use API versioning
    c.DocInclusionPredicate((version, apiDescription) =>
    {
        if (!apiDescription.TryGetMethodInfo(out var method)) {

            return false;
        }
        var versions = method.DeclaringType?.GetCustomAttributes(true)
            .OfType<ApiVersionAttribute>()
            .SelectMany(attr => attr.Versions);

        return versions?.Any(v => $"v{v.ToString()}" == version) ?? false;

    });
});

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),                     // Usually the API project
    typeof(CreateShoppingCartCommandHandler).Assembly,               // Catalog.Application
};

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
});


//Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

//Register Application Services
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg =>
{
    cfg.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl"));
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI( c=> 
    { 
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "Basket.API v2");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
