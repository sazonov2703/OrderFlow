namespace Application.UseCases.Orders.DTOs;

public record UpdateOrderStatusCommandDto(
    Guid WorkspaceId,
    Guid OrderId,
    string Status
    );