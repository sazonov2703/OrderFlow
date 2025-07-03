using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record CreateProductCommand(Guid UserId, CreateProductCommandDto CreateProductCommandDto) : IRequest<Guid>;

