using MediatR;

namespace Application.UseCases.OrderItems.Commands.Requests;

public record CreateOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity
    ) : IRequest<Guid>;