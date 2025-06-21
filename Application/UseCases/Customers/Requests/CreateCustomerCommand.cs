using Application.UseCases.Customers.DTOs;
using MediatR;

namespace Application.UseCases.Customers.Requests;

public record CreateCustomerCommand(Guid UserId, CreateCustomerCommandDto CreateCustomerCommandDto) : IRequest<Guid>;
