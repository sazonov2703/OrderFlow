using Application.Interfaces.Repositories.Write;
using Application.Services.Customers;
using Application.Services.OrderItems;
using Application.Services.Workspaces;
using Application.UseCases.Orders.Requests;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Orders.Handlers;

public class CreateOrderCommandHandler(
    IOrderWriteRepository orderWriteRepository,
    CustomerBuilderService customerResolverService,
    WorkspaceAccessService workspaceAccessService,
    OrderItemBuilderService orderItemBuilderService
    ) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateOrderCommandDto;

        // Get and validate the workspace
        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, dto.WorkspaceId, cancellationToken);

        // Get or create the customer
        var customer = await customerResolverService.GetOrCreateCustomerAsync(
            workspace, dto.CustomerId, dto.FirstName, dto.LastName,
            dto.Patronymic, dto.Email, dto.PhoneNumbers, dto.Links, cancellationToken);

        // Create the shipping address
        var shippingAddress = new ShippingAddress(dto.ShippingRecipentName, dto.ShippingCountry, dto.ShippingCity,
            dto.ShippingStreet, dto.ShippingHouseNumber, dto.ShippingFlatNumber, dto.ShippingZipCode);
        
        // Create order without items
        var order = new Order(workspace, null, customer, shippingAddress, dto.ShippingCost, dto.Description);

        // Create order items and add them to the order
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
            
        // Save the order
        await orderWriteRepository.AddAsync(order, cancellationToken);
        
        // Save the changes
        await orderWriteRepository.SaveChangesAsync(cancellationToken);
        
        // Return the order id
        return order.Id;
    }
}