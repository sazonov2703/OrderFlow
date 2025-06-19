using Application.UseCases.Users.Commands.Requests;
using Application.UseCases.Users.Queries.Requests;
using Infrastructure.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Infrastructure.DTOs.Auth.RegisterRequest;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : Controller
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new RegisterUserCommand(request.Username, request.Email, request.Password),  
            cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginUserQuery(request.Email, request.Password), 
            cancellationToken);

        return Ok(result);
    }
}