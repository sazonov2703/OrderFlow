using Application.Interfaces.Repositories.Write;
using Application.Services.Customers;
using Application.Services.OrderItems;
using Application.Services.Orders;
using Application.Services.Workspaces;
using Application.UseCases.Orders.Requests;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Orders.Handlers;

public class UpdateOrderCommandHandler(
    IOrderWriteRepository orderWriteRepository,
    WorkspaceAccessService workspaceAccessService,
    CustomerBuilderService customerResolverService,
    OrderItemBuilderService orderItemBuilderService,
    OrderAccessService orderAccessService
    ) : IRequestHandler<UpdateOrderCommand, Guid>
{
    public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateOrderCommandDto;

        // Get and validate the workspace
        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, dto.WorkspaceId, cancellationToken);
        
        // Get and validate the order
        var order = await orderAccessService.GetAndValidateOrderById(
            workspace.Id, dto.OrderId, cancellationToken);

        // Get or create the customer
        var customer = await customerResolverService.GetOrCreateCustomerAsync(
            workspace, dto.CustomerId, dto.FirstName, dto.LastName,
            dto.Patronymic, dto.Email, dto.PhoneNumbers, dto.Links, cancellationToken);

        // Create the shipping address
        var shippingAddress = new ShippingAddress(dto.ShippingRecipentName, dto.ShippingCountry, dto.ShippingCity,
            dto.ShippingStreet, dto.ShippingHouseNumber, dto.ShippingFlatNumber, dto.ShippingZipCode);

        // Update the order
        order.Update(customer, shippingAddress, dto.ShippingCost, dto.Description);
        order.ClearOrderItems();
        foreach (var orderItemDto in dto.OrderItems)
        {
            await orderItemBuilderService.BuildOrderItemAndAttachToOrderAsync(
                order, 
                orderItemDto.ProductId, 
                orderItemDto.ProductName, 
                orderItemDto.ProductUnitPrice,
                orderItemDto.ProductDescription,
                orderItemDto.ProductImageUrl,
                orderItemDto.Quantity,
                cancellationToken);
        }
            
        // Update the order in db
        await orderWriteRepository.UpdateAsync(order, cancellationToken);
        
        // Save the changes
        await orderWriteRepository.SaveChangesAsync(cancellationToken);
        
        // Return the order id
        return order.Id;
    }
}