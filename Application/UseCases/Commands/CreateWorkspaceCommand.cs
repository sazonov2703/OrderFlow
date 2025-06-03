using MediatR;

namespace Application.UseCases.Commands;

public record CreateWorkspaceCommand(
    Guid UserId, string Name
    ) : IRequest<Guid>;