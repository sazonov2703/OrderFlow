using Application.UseCases.Users.Commands.DTOs;
using Application.UseCases.Users.Commands.Requests;
using Application.UseCases.Users.Queries.DTOs;
using Application.UseCases.Users.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : Controller
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new RegisterUserCommand(registerUserDto),  
            cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginUserQuery(loginUserDto), 
            cancellationToken);

        return Ok(result);
    }
}