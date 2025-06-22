using Application.UseCases.Orders.DTOs;
using MediatR;

namespace Application.UseCases.Orders.Requests;

public record DeleteOrderCommand(Guid UserId, Guid WorkspaceId, Guid OrderId) : IRequest<Unit>;