using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record UpdateOrderStatusCommand(
    Guid UserId, UpdateOrderStatusCommandDto UpdateOrderStatusCommandDto
    ) : IRequest<Guid>;