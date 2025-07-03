using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record UpdateOrderCommand(Guid UserId, UpdateOrderCommandDto UpdateOrderCommandDto) : IRequest<Guid>;