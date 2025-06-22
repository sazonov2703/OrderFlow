using Application.UseCases.Orders.DTOs;
using MediatR;

namespace Application.UseCases.Orders.Requests;

public record UpdateOrderStatusCommand(
    Guid UserId, UpdateOrderStatusCommandDto UpdateOrderStatusCommandDto
    ) : IRequest<Guid>;