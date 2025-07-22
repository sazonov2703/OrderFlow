using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record DeleteUserFromWorkspaceByUsernameCommand(
    Guid UserId, 
    DeleteUserFromWorkspaceByUsernameCommandDto DeleteUserFromWorkspaceByUsernameCommandDto
    ) : IRequest<Guid>;