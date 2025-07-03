namespace Application.DTOs;

public record UpdateOrderStatusCommandDto(
    Guid WorkspaceId,
    Guid OrderId,
    string Status
    );