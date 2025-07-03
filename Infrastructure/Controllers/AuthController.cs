using Application.DTOs;
using Application.UseCases.Commands.Requests;
using Application.UseCases.Quiaries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : BaseController
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