namespace Application.DTOs;

public record OrderItemDto(
    Guid? ProductId, 
    string? ProductName,
    decimal? ProductUnitPrice,
    string? ProductDescription,
    string? ProductImageUrl,
    int? Quantity
);