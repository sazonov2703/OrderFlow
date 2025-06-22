using Application.Interfaces.Repositories.Write;
using Application.Services.Workspaces;
using Application.UseCases.Workspaces.Requests;
using MediatR;

namespace Application.UseCases.Workspaces.Handlers;

public class DeleteWorkspaceCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    IWorkspaceWriteRepository workspaceWriteRepository
    ) : IRequestHandler<DeleteWorkspaceCommand, Unit>
{
    public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
    {
        // Get and validate the workspace
        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, request.WorkspaceId, cancellationToken);
        
        // Delete the workspace
        await workspaceWriteRepository.DeleteAsync(request.WorkspaceId, cancellationToken);
        
        // Save the changes
        await workspaceWriteRepository.SaveChangesAsync(cancellationToken);
        
        // Return unit
        return Unit.Value;
    }
}