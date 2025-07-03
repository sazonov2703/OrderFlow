using System.Text;
using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.Services.Customers;
using Application.Services.OrderItems;
using Application.Services.Orders;
using Application.Services.Products;
using Application.Services.Workspaces;
using Infrastructure;
using Infrastructure.Dal;
using Infrastructure.Dal.Repositories.Read;
using Infrastructure.Dal.Repositories.Write;
using Infrastructure.Filters;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AssemblyReference = Application.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token, starts from 'Bearer ', for example: Bearer abcdef12345..."
    });

    options.OperationFilter<AuthorizeCheckOperationFilter>();
});


// Add MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

// Add JWT authentification
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["JwtSettings:SecretKey"] 
                    ?? throw new KeyNotFoundException($"Cannot find key 'JwtSettings:SecretKey' in configuration"))
            )
        };
    });

// Add authorization
builder.Services.AddAuthorization();

builder.Services.AddTransient<WorkspaceAccessService>();
builder.Services.AddTransient<CustomerAccessService>();
builder.Services.AddTransient<CustomerBuilderService>();
builder.Services.AddTransient<OrderItemBuilderService>();
builder.Services.AddTransient<OrderAccessService>();
builder.Services.AddTransient<ProductAccessService>();
builder.Services.AddTransient<ProductBuilderService>();
builder.Services.AddTransient<WorkspaceAccessService>();

// Add database context
builder.Services.AddDbContext<OrderFlowDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o =>
        {
            o.CommandTimeout(10);
            o.EnableRetryOnFailure(3);
        }
        )
    );

// Add services
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Add repositories
builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
builder.Services.AddScoped<IOrderReadRepository, OrderReadRepository>();
builder.Services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
builder.Services.AddScoped<IProductReadRepository, ProductReadRepository>();
builder.Services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
builder.Services.AddScoped<IUserWriteRepository, UserWriteRepository>();
builder.Services.AddScoped<IWorkspaceReadRepository, WorkspaceReadRepository>();
builder.Services.AddScoped<IWorkspaceWriteRepository, WorkspaceWriteRepository>();
builder.Services.AddScoped<IOrderItemReadRepository, OrderItemReadRepository>();
builder.Services.AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Add exception handling middleware
app.UseExceptionHandler("/Error");

// Enable middleware to serve generated Swagger as a JSON endpoint.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();