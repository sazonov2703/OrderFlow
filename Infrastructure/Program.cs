using System.Text;
using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Infrastructure.Dal;
using Infrastructure.Dal.Repositories.Read;
using Infrastructure.Dal.Repositories.Write;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

// Add authorization
builder.Services.AddAuthorization();

var app = builder.Build();

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