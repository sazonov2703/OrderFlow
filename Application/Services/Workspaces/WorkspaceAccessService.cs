using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Application.Services.Workspaces;

public class WorkspaceAccessService(
    IUserReadRepository userReadRepository, 
    IWorkspaceReadRepository workspaceReadRepository)
{
    public async Task<Workspace> GetAndValidateWorkspaceById(
        Guid userId, Guid workspaceId, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
            throw new InvalidOperationException($"User {userId} not found");

        var workspaces = await workspaceReadRepository.GetByUserAsync(user.Id, cancellationToken);

        var workspace = workspaces.FirstOrDefault(w => w.Id == workspaceId);
        if (workspace == null)
            throw new InvalidOperationException($"User {user.Id} does not have access to workspace {workspaceId}");

        return workspace;
    }
}