using Application.UseCases.Orders.Commands.DTOs;
using MediatR;

namespace Application.UseCases.Orders.Commands.Requests;

public record CreateOrderCommand(Guid UserId, CreateOrderDto CreateOrderDto) : IRequest<Guid>;
