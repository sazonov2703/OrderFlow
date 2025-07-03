namespace Application.DTOs;

public record UpdateOrderCommandDto(
    Guid WorkspaceId,
    Guid OrderId,
    
    List<OrderItemDto> OrderItems,
    
    Guid? CustomerId,
    string? FirstName,
    string? LastName,
    string? Patronymic,
    string? Email,
    string? PhoneNumber,
    string? Link,
    
    string? ShippingRecipentName,
    string? ShippingCountry,
    string? ShippingCity,
    string? ShippingStreet,
    string? ShippingHouseNumber,
    string? ShippingFlatNumber,
    string? ShippingZipCode,
        
    decimal ShippingCost,
    string Description
);