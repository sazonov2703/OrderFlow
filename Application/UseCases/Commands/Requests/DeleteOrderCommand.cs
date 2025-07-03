using MediatR;

namespace Application.UseCases.Commands.Requests;

public record DeleteOrderCommand(Guid UserId, Guid WorkspaceId, Guid OrderId) : IRequest<Unit>;