using Application.UseCases.Orders.DTOs;
using MediatR;

namespace Application.UseCases.Orders.Requests;

public record CreateOrderCommand(Guid UserId, CreateOrderCommandDto CreateOrderCommandDto) : IRequest<Guid>;
