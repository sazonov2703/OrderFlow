using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Workspaces.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Workspaces.Commands.Handlers;

public class CreateWorkspaceCommandHandler(
    IWorkspaceWriteRepository workspaceWriteRepository, 
    IUserReadRepository userReadRepository
    ) : IRequestHandler<CreateWorkspaceCommand, Guid>
{
    public async Task<Guid> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null) throw new InvalidOperationException($"User {request.UserId} does not exist.");

        var workspace = new Workspace(
            request.Name, 
            user
            );
        
        await workspaceWriteRepository.AddAsync(workspace, cancellationToken);
        
        await workspaceWriteRepository.SaveChangesAsync(cancellationToken);
        
        return workspace.Id;
    }
}