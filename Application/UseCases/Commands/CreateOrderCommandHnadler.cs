using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateOrderCommandHnadler(
    IWorkspaceReadRepository workspaceReadRepository,
    IOrderWriteRepository orderWriteRepository,
    ICustomerReadRepository customerReadRepository,
    IOrderItemReadRepository orderItemReadRepository
    ) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var workspace = await workspaceReadRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);
        
        if (workspace == null) throw new InvalidOperationException($"Workspace {request.WorkspaceId} does not exist");
        
        List<OrderItem> orderItems = new List<OrderItem>();

        foreach (var orderItemId in request.OrderItemsId)
        {
            var orderItem = await orderItemReadRepository. GetByIdAsync(orderItemId, cancellationToken);

            orderItems.Add(orderItem);
        }
        
        var customer = await customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        
        var shippingAddress = new ShippingAddress(
            request.ShippingRecipentName, 
            request.ShippingCountry, 
            request.ShippingCity,
            request.ShippingStreet,
            request.ShippingHouseNumber,
            request.ShippingFlatNumber,
            request.ShippingZipCode
            );
        
        var order = new Order(
            workspace, 
            orderItems, 
            customer, 
            shippingAddress, 
            request.ShippingCost, 
            request.Description,
            request.Deadline
            );
        
        await orderWriteRepository.AddAsync(order, cancellationToken);
        
        await orderWriteRepository.SaveChangesAsync(cancellationToken);
        
        return order.Id;
    }
}