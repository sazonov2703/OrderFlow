using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Filters;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize = 
            context.MethodInfo.DeclaringType != null && 
            (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() 
             || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

        var allowAnonymous = context.MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>().Any();

        if (hasAuthorize && !allowAnonymous)
        {
            if (operation.Security == null)
                operation.Security = new List<OpenApiSecurityRequirement>();

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                }] = new string[] { }
            });
        }
    }
}