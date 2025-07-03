using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record CreateWorkspaceCommand(Guid UserId, CreateWorkspaceDto CreateWorkspaceDto) : IRequest<Guid>;