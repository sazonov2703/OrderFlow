using System.Security.Claims;
using Application.UseCases.Orders.Commands.Requests;
using Infrastructure.DTOs.Order;
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
        [FromBody] OrderRequest request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();       
        }
        
        var workspaceId = request.WorkspaceId;
        
        var result = await mediator.Send(new CreateOrderCommand(
            Guid.Parse(userId), workspaceId, request.OrderItemsInOrder, request.CustomerInOrder,
            request.ShippingInOrder, request.ShippingCost, request.Description, request.Deadline
            ), cancellationToken);
        
        return Ok(result);
    }
}