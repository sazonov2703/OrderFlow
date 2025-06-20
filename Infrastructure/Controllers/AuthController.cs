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
        [FromBody] RegisterUserCommandDto registerUserCommandDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new RegisterUserCommand(registerUserCommandDto),  
            cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserQueryDto loginUserQueryDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginUserQuery(loginUserQueryDto), 
            cancellationToken);

        return Ok(result);
    }
}