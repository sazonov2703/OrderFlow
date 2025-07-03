using Application.Interfaces.Repositories.Write;
using Application.Services.Orders;
using Application.Services.Workspaces;
using Application.UseCases.Commands.Requests;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

public class DeleteOrderCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    OrderAccessService orderAccessService,
    IOrderWriteRepository orderWriteRepository
    ) : IRequestHandler<DeleteOrderCommand, Unit>
{
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        // Get and validate the workspace
        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, request.WorkspaceId, cancellationToken);
        
        // Get and validate the order
        var order = await orderAccessService.GetAndValidateOrderById(
            workspace.Id, request.OrderId, cancellationToken);
        
        // Delete the order
        await orderWriteRepository.DeleteAsync(request.OrderId, cancellationToken);
        
        // Save the changes
        await orderWriteRepository.SaveChangesAsync(cancellationToken);
        
        // Return unit
        return Unit.Value;
    }
}