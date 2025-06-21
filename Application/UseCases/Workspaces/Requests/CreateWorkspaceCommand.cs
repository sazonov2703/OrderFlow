using Application.UseCases.Workspaces.DTOs;
using MediatR;

namespace Application.UseCases.Workspaces.Requests;

public record CreateWorkspaceCommand(Guid UserId, CreateWorkspaceDto CreateWorkspaceDto) : IRequest<Guid>;