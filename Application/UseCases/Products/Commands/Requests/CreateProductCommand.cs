using MediatR;

namespace Application.UseCases.Products.Commands.Requests;

public record CreateProductCommand(
    Guid WorkspaceId, 
    string Name, 
    string Description,
    decimal UnitPrice
    ) : IRequest<Guid>;
