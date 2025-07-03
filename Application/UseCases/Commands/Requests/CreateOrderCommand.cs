using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record CreateOrderCommand(Guid UserId, CreateOrderCommandDto CreateOrderCommandDto) : IRequest<Guid>;
