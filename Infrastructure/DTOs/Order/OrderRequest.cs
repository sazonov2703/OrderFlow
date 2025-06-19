using Application.DTOs;

namespace Infrastructure.DTOs.Order;

public record OrderRequest(
    Guid WorkspaceId,
    List<OrderItemInOrder> OrderItemsInOrder,
    CustomerInOrder CustomerInOrder,
    ShippingInOrder ShippingInOrder,
    decimal ShippingCost,
    string Description,
    DateTime Deadline
    );