namespace Application.DTOs;

public record OrderItemInOrder(
    Guid? ProductId, 
    string Name,
    string Description,
    decimal UnitPrice,
    int Quantity
    );