namespace Application.UseCases.Orders.DTOs;

public record UpdateOrderCommandDto(
    Guid WorkspaceId,
    Guid OrderId,
    
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
    string Description
);