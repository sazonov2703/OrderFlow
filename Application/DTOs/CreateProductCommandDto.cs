namespace Application.DTOs;

public record CreateProductCommandDto(
    Guid WorkspaceId, string Name, string Description, decimal UnitPrice, string ImageUrl);