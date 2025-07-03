using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record CreateCustomerCommand(Guid UserId, CreateCustomerCommandDto CreateCustomerCommandDto) : IRequest<Guid>;
