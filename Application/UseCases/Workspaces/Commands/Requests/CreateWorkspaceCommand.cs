using MediatR;

namespace Application.UseCases.Workspaces.Commands.Requests;

public record CreateWorkspaceCommand(
    Guid UserId, 
    string Name
    ) : IRequest<Guid>;