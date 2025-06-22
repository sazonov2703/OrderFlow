using Application.Interfaces.Repositories.Write;
using Application.Services.Products;
using Domain.Entities;

namespace Application.Services.OrderItems;

public class OrderItemBuilderService(
    ProductBuilderService productBuilderService,
    IOrderItemWriteRepository orderItemWriteRepository)
{
    public async Task BuildOrderItemAndAttachToOrderAsync(Workspace workspace,
        Order order,
        Guid? productId,
        string name,
        decimal unitPrice,
        string description,
        string? imageUrl,
        int quantity,
        CancellationToken cancellationToken)
    {
        var product = await productBuilderService.GetOrCreateProduct(
            workspace,
            productId ?? Guid.Empty,
            name,
            description,
            unitPrice,
            imageUrl,
            cancellationToken);

        var orderItem = new OrderItem(workspace, product, order, quantity);
        orderItem.UpdateFields(name, unitPrice, quantity);

        order.AddOrderItem(orderItem);
        await orderItemWriteRepository.AddAsync(orderItem, cancellationToken);
    }
}