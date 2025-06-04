using MediatR;

namespace Application.UseCases.Commands;

public record CreateProductCommand(
    Guid WorkspaceId, 
    string Name, 
    string Description,
    decimal UnitPrice
    ) : IRequest<Guid>;
