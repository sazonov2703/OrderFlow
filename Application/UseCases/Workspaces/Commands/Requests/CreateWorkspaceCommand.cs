using Application.UseCases.Workspaces.Commands.DTOs;
using MediatR;

namespace Application.UseCases.Workspaces.Commands.Requests;

public record CreateWorkspaceCommand(Guid UserId, CreateWorkspaceDto CreateWorkspaceDto) : IRequest<Guid>;