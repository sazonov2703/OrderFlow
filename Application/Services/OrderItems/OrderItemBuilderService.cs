using Application.Interfaces.Repositories.Write;
using Application.Services.Products;
using Domain.Entities;

namespace Application.Services.OrderItems;

public class OrderItemBuilderService(
    ProductBuilderService productBuilderService,
    IOrderItemWriteRepository orderItemWriteRepository)
{
    public async Task BuildOrderItemAndAttachToOrderAsync(
        Order order,
        Guid? productId,
        string? name,
        decimal? unitPrice,
        string? description,
        string? imageUrl,
        int? quantity,
        CancellationToken cancellationToken)
    {
        var product = await productBuilderService.GetOrCreateProduct(
            order.Workspace,
            productId ?? Guid.Empty,
            name ?? string.Empty,
            description ?? string.Empty,
            unitPrice ?? 0,
            imageUrl ?? string.Empty,
            cancellationToken);

        var orderItem = new OrderItem(product, order, quantity ?? 0);
        orderItem.UpdateFields(name ?? string.Empty, unitPrice ?? 0, quantity ?? 0);

        order.AddOrderItem(orderItem);
        await orderItemWriteRepository.AddAsync(orderItem, cancellationToken);
    }
}