using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record AddUserToWorkspaceByUsernameCommand(Guid UserId, 
    AddUserToWorkspaceByUsernameCommandDto AddUserToWorkspaceByUsernameCommandDto) : IRequest<Guid>;