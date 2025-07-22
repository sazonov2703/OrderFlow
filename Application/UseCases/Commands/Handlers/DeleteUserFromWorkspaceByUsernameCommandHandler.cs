using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.Services.Workspaces;
using Application.UseCases.Commands.Requests;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

public class DeleteUserFromWorkspaceByUsernameCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    IWorkspaceWriteRepository workspaceWriteRepository,
    IUserReadRepository userReadRepository
    ) : IRequestHandler<DeleteUserFromWorkspaceByUsernameCommand, Guid>
{
    public async Task<Guid> Handle(
        DeleteUserFromWorkspaceByUsernameCommand request, CancellationToken cancellationToken)
    {
        var dto = request.DeleteUserFromWorkspaceByUsernameCommandDto;
        
        var user = await userReadRepository.GetUserByUsernameAsync(dto.Username, cancellationToken);

        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            user.Id, dto.WorkspaceId, cancellationToken);
        
        var role = workspace.UserWorkspaces.FirstOrDefault(x => x.UserId == user.Id).Role
            ?? throw new InvalidOperationException($"User {user.Id} does not in workspace {dto.WorkspaceId}");
        
        if (!role.HasAtLeastRole(WorkspaceRole.Admin))
        {
            throw new InvalidOperationException(
                $"User {user.Id} does not have admin role in workspace {dto.WorkspaceId}");
        }
        
        workspace.RemoveUser(user);
        
        await workspaceWriteRepository.UpdateAsync(workspace, cancellationToken);
        
        await workspaceWriteRepository.SaveChangesAsync(cancellationToken);
        
        return workspace.Id;
    }
}