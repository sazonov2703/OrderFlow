using Application.Interfaces.Repositories.Write;
using Application.Services.Orders;
using Application.Services.Workspaces;
using Application.UseCases.Commands.Requests;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

public class UpdateOrderStatusCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    OrderAccessService orderAccessService,
    IOrderWriteRepository orderWriteRepository
    ) : IRequestHandler<UpdateOrderStatusCommand, Guid>
{
    public async Task<Guid> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateOrderStatusCommandDto;
        
        // Get and validate the workspace
        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, dto.WorkspaceId, cancellationToken);
        
        // Get and validate the order
        var order = await orderAccessService.GetAndValidateOrderById(workspace.Id, dto.OrderId, cancellationToken);

        // Create status from a name
        var status = Status.FromName(dto.Status);
        
        // Update the order status
        order.UpdateStatus(status);
        
        // Update the order in db
        await orderWriteRepository.UpdateAsync(order, cancellationToken);
        
        // Save the changes
        await orderWriteRepository.SaveChangesAsync(cancellationToken);
        
        // Return the order id
        return order.Id;
    }
}