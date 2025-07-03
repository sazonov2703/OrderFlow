using MediatR;

namespace Application.UseCases.Commands.Requests;

public record DeleteWorkspaceCommand(Guid UserId, Guid WorkspaceId) : IRequest<Unit>;
