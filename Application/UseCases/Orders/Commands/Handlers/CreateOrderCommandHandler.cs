using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Orders.Commands.DTOs;
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
        var dto = request.CreateOrderDto;
        
        var workspace = await GetAndValidateWorkspace(request.UserId, dto.WorkspaceId, cancellationToken);
    
        var orderItems = await BuildOrderItems(dto.OrderItems, workspace, cancellationToken);
    
        var customer = await GetOrCreateCustomer(dto, workspace, cancellationToken);
    
        var shippingAddress = BuildShippingAddress(dto);

        var order = new Order(
            workspace, orderItems, customer, shippingAddress, 
            dto.ShippingCost, dto.Description, dto.CloseDate);

        foreach (var orderItem in orderItems)
        {
            orderItem.SetOrder(order);
            orderItem.Product.AddToOrderItem(orderItem);
        }

        await orderWriteRepository.AddAsync(order, cancellationToken);

        foreach (var item in orderItems)
        {
            await orderItemWriteRepository.AddAsync(item, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
    
    private async Task<Workspace> GetAndValidateWorkspace(Guid userId, Guid workspaceId, CancellationToken cancellationToken)
    {
        var workspaces = await workspaceReadRepository.GetByUserAsync(userId, cancellationToken);
        var workspace = workspaces.FirstOrDefault(w => w.Id == workspaceId);

        if (workspace == null)
        {
            throw new KeyNotFoundException($"User {userId} does not have access to workspace {workspaceId}");
        }

        return workspace;
    }
    
    private async Task<Customer?> GetOrCreateCustomer(CreateOrderDto dto, Workspace workspace, CancellationToken cancellationToken)
    {
        Customer customer;

        if (IsCustomerEmpty(dto))
        {
            return null;
        }
        
        if (dto.CustomerId is { } customerId && customerId != Guid.Empty)
        {
            customer = await customerReadRepository.GetByIdAsync(customerId, cancellationToken);

            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with id {dto.CustomerId} not found");
            }
        }

        else
        {
            customer = new Customer(
                workspace,
                dto.FirstName,
                dto.LastName,
                dto.Patronymic,
                dto.Email,
                dto.PhoneNumbers,
                dto.Links
            );
        }

        await customerWriteRepository.AddAsync(customer, cancellationToken);
        
        return customer;
    }
    
    private async Task<List<OrderItem>> BuildOrderItems(List<OrderItemDto> orderItemDto, Workspace workspace, CancellationToken cancellationToken)
    {
        var result = new List<OrderItem>();

        foreach (var item in orderItemDto)
        {
            Product product;

            if (item.ProductId is { } productId && productId != Guid.Empty)
            {
                product = await productReadRepository.GetByIdAsync(productId, cancellationToken);
                
                if (product == null)
                    throw new KeyNotFoundException($"Product with id {item.ProductId} not found");
            }
            
            else
            {
                product = new Product(
                    workspace, item.ProductName, item.ProductDescription, item.ProductUnitPrice, item.ProductImageUrl);
                await productWriteRepository.AddAsync(product, cancellationToken);
            }

            var orderItem = new OrderItem(product, null, item.Quantity);
            orderItem.UpdateFields(item.ProductName, item.ProductUnitPrice, item.Quantity);

            result.Add(orderItem);
        }

        return result;
    }
    
    private ShippingAddress BuildShippingAddress(CreateOrderDto dto)
    {
        return new ShippingAddress(
            dto.ShippingRecipentName,
            dto.ShippingCountry,
            dto.ShippingCity,
            dto.ShippingStreet,
            dto.ShippingHouseNumber,
            dto.ShippingFlatNumber,
            dto.ShippingZipCode
        );
    }
    
    private bool IsCustomerEmpty(CreateOrderDto dto)
    {
        return dto.CustomerId == Guid.Empty
               && string.IsNullOrWhiteSpace(dto.FirstName)
               && string.IsNullOrWhiteSpace(dto.LastName)
               && string.IsNullOrWhiteSpace(dto.Patronymic)
               && string.IsNullOrWhiteSpace(dto.Email)
               && (dto.PhoneNumbers == null || dto.PhoneNumbers.Count == 0)
               && (dto.Links == null || dto.Links.Count == 0);
    }
}