using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateOrderItemCommandHandler(
    IOrderItemWriteRepository orderItemWriteRepository,
    IOrderReadRepository orderReadRepository,
    IProductReadRepository productReadRepository
    ) : IRequestHandler<CreateOrderItemCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productReadRepository.GetByIdAsync(request.ProductId, cancellationToken);
        
        var order = await orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        
        if (order is null) throw new InvalidOperationException($"Order {request.OrderId} does not exist");
        
        var orderItem = new OrderItem(
            product, 
            order, 
            request.Quantity
            );
        
        await orderItemWriteRepository.AddAsync(orderItem, cancellationToken);
        
        await orderItemWriteRepository.SaveChangesAsync(cancellationToken);
        
        return orderItem.Id;
    }
}