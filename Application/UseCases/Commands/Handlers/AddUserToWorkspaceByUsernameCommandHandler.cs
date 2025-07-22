using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.Services.Workspaces;
using Application.UseCases.Commands.Requests;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

public class AddUserToWorkspaceByUsernameCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    IWorkspaceWriteRepository workspaceWriteRepository,
    IUserReadRepository userReadRepository
    ) : IRequestHandler<AddUserToWorkspaceByUsernameCommand, Guid>
{
    public async Task<Guid> Handle(AddUserToWorkspaceByUsernameCommand request, CancellationToken cancellationToken)
    {
        var dto = request.AddUserToWorkspaceByUsernameCommandDto;

        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, dto.WorkspaceId, cancellationToken);;
        
        var user = await userReadRepository.GetUserByUsernameAsync(dto.Username, cancellationToken);
        
        workspace.AddUser(user, WorkspaceRole.From(dto.Role));
        
        await workspaceWriteRepository.UpdateAsync(workspace, cancellationToken);
        
        await workspaceWriteRepository.SaveChangesAsync(cancellationToken);
        
        return workspace.Id;
    }
}