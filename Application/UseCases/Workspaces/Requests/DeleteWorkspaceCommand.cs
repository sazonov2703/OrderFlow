using Application.UseCases.Workspaces.DTOs;
using MediatR;

namespace Application.UseCases.Workspaces.Requests;

public record DeleteWorkspaceCommand(Guid UserId, Guid WorkspaceId) : IRequest<Unit>;
