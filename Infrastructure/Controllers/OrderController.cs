using Application.DTOs;
using Application.UseCases.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(IMediator mediator) : AuthorizedBaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderCommandDto createOrderCommandDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateOrderCommand(
            UserId, createOrderCommandDto), cancellationToken);
        
        return Ok(result);
    }

    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeOrderStatus(
        [FromBody] UpdateOrderStatusCommandDto updateOrderStatusCommandDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateOrderStatusCommand(
            UserId, updateOrderStatusCommandDto), cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateOrder(
        [FromBody] UpdateOrderCommandDto updateOrderCommandDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateOrderCommand(
            UserId, updateOrderCommandDto), cancellationToken);
        
        return Ok(result);   
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteOrder(Guid orderId, Guid workspaceId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteOrderCommand(
            UserId, orderId, workspaceId), cancellationToken);
        
        return Ok(result);
    }
}