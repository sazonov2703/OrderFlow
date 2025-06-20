namespace Application.UseCases.Orders.Commands.DTOs;

public record CreateOrderDto(
    Guid WorkspaceId,
    
    List<OrderItemDto> OrderItems,
    
    Guid? CustomerId,
    string? FirstName,
    string? LastName,
    string? Patronymic,
    string? Email,
    List<string>? PhoneNumbers,
    List<string>? Links,
    
    string? ShippingRecipentName,
    string? ShippingCountry,
    string? ShippingCity,
    string? ShippingStreet,
    string? ShippingHouseNumber,
    string? ShippingFlatNumber,
    string? ShippingZipCode,
        
    decimal ShippingCost,
    string Description,
    DateTime CloseDate
    );
    
public record OrderItemDto(
    Guid? ProductId, 
    string ProductName,
    decimal ProductUnitPrice,
    string ProductDescription,
    string? ProductImageUrl,
    int Quantity
    );