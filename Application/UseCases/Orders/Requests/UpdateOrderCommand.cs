using Application.UseCases.Orders.DTOs;
using MediatR;

namespace Application.UseCases.Orders.Requests;

public record UpdateOrderCommand(Guid UserId, UpdateOrderCommandDto UpdateOrderCommandDto) : IRequest<Guid>;