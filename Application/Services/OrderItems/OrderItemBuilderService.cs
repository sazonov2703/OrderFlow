using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Orders.DTOs;
using Domain.Entities;

namespace Application.Services.OrderItems;

public class OrderItemBuilderService(
    IProductReadRepository productReadRepository,
    IProductWriteRepository productWriteRepository,
    IOrderItemWriteRepository orderItemWriteRepository)
{
    public async Task BuildOrderItemsAndAttachToOrderAsync(
        Workspace workspace, Order order, List<OrderItemDto> orderItemDtos, CancellationToken cancellationToken)
    {
        foreach (var dto in orderItemDtos)
        {
            Product product;

            if (dto.ProductId is { } productId && productId != Guid.Empty)
            {
                product = await productReadRepository.GetByIdAsync(productId, cancellationToken)
                          ?? throw new InvalidOperationException($"Product {productId} does not exist");
            }
            else
            {
                product = new Product(
                    workspace,
                    dto.ProductName,
                    dto.ProductDescription,
                    dto.ProductUnitPrice,
                    dto.ProductImageUrl);

                await productWriteRepository.AddAsync(product, cancellationToken);
            }

            var orderItem = new OrderItem(workspace, product, order, dto.Quantity);
            orderItem.UpdateFields(dto.ProductName, dto.ProductUnitPrice, dto.Quantity);

            await orderItemWriteRepository.AddAsync(orderItem, cancellationToken);
            order.AddOrderItem(orderItem);
        }
    }
}