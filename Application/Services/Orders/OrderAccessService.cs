using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Application.Services.Orders;

public class OrderAccessService(
    IOrderReadRepository orderReadRepository)
{
    public async Task<Order> GetAndValidateOrderById(
        Guid workspaceId, Guid orderId, CancellationToken cancellationToken)
    {
        var order = await orderReadRepository.GetByIdAsync(orderId, cancellationToken);
        
        if (order == null)
            throw new InvalidOperationException($"Order {orderId} not found");
        
        if (order.WorkspaceId != workspaceId)
            throw new UnauthorizedAccessException($"Order {orderId} does not belong to workspace {workspaceId}");

        return order;
    }
}