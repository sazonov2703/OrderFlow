using System.Security.Claims;
using Application.UseCases.Orders.Commands.DTOs;
using Application.UseCases.Orders.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(IMediator mediator) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderDto createOrderDto, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();       
        }
        
        var result = await mediator.Send(new CreateOrderCommand(
            Guid.Parse(userId), createOrderDto), cancellationToken);
        
        return Ok(result);
    }
}