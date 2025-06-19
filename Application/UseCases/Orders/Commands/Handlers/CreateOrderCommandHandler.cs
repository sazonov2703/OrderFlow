using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Orders.Commands.Requests;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Orders.Commands.Handlers;

public class CreateOrderCommandHandler(
    IUnitOfWork unitOfWork,
    IWorkspaceReadRepository workspaceReadRepository,
    IProductWriteRepository productWriteRepository,
    IProductReadRepository productReadRepository,
    ICustomerWriteRepository customerWriteRepository,
    IOrderWriteRepository orderWriteRepository,
    ICustomerReadRepository customerReadRepository,
    IOrderItemWriteRepository orderItemWriteRepository
    ) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Getting workspace by user id
        var workspaces = await workspaceReadRepository.GetByUserAsync(request.UserId, cancellationToken);

        var workspace = workspaces.Where(w => w.Id == request.WorkspaceId).FirstOrDefault();

        if (workspace == null)
        {
            throw new KeyNotFoundException(
                $"User {request.UserId} does not have access to workspace {request.WorkspaceId}");
        }
        
        // Getting products and order items
        var orderItems = new List<OrderItem>();

        foreach (var orderItemInOrder in request.OrderItemsInOrder)
        {
            if (orderItemInOrder.ProductId != Guid.Empty)
            {
                var product = await productReadRepository.GetByIdAsync((Guid)orderItemInOrder.ProductId, cancellationToken);
                var orderItem = new OrderItem(product, null, orderItemInOrder.Quantity);
                orderItems.Add(orderItem);
            }
            
            else if (orderItemInOrder.ProductId == Guid.Empty)
            {
                var product = new Product(
                    workspace, orderItemInOrder.Name, orderItemInOrder.Description, orderItemInOrder.UnitPrice
                    );
                
                await productWriteRepository.AddAsync(product, cancellationToken);
                
                var orderItem = new OrderItem(product, null, orderItemInOrder.Quantity);
                orderItems.Add(orderItem);
            }
        }
        
        // Getting customer
        if (request.CustomerInOrder.CustomerId == Guid.Empty)
        {
            var newCustomer = new Customer(
                workspace, request.CustomerInOrder.firstName, 
                request.CustomerInOrder.lastName, request.CustomerInOrder.patronymic, 
                request.CustomerInOrder.email, request.CustomerInOrder.phoneNumbers,
                request.CustomerInOrder.links
                );
            
            await customerWriteRepository.AddAsync(newCustomer, cancellationToken);
        }

        var customer = await customerReadRepository.GetByIdAsync(
                (Guid)request.CustomerInOrder.CustomerId, cancellationToken);
        
        // Getting shipping address
        var shippingAddress = new ShippingAddress(
            request.ShippingInOrder.ShippingRecipentName, request.ShippingInOrder.ShippingCountry, 
            request.ShippingInOrder.ShippingCity, request.ShippingInOrder.ShippingStreet, 
            request.ShippingInOrder.ShippingHouseNumber, request.ShippingInOrder.ShippingFlatNumber, 
            request.ShippingInOrder.ShippingZipCode)
            ;
        
        // Creating order and add Order to OrderItems
        var order = new Order(workspace, orderItems, customer, shippingAddress, request.ShippingCost, request.Description, request.Deadline);

        foreach (var orderItem in orderItems)
        {
            orderItem.SetOrder(order);
            orderItem.Product.AddToOrder(order);
        }
        
        // Adding order and orderItems to db
        await orderWriteRepository.AddAsync(order, cancellationToken);

        foreach (var orderItem in orderItems)
        {
            await orderItemWriteRepository.AddAsync(orderItem, cancellationToken);
        }
        
        // UnitOfWork instead of multiply SaveChangesAsync(for ACID)
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return order.Id;
    }
}