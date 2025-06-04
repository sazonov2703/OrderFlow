using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public record CreateOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    int Quantity
    ) : IRequest<Guid>;