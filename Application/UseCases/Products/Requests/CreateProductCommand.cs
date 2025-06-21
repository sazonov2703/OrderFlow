using Application.UseCases.Products.DTOs;
using MediatR;

namespace Application.UseCases.Products.Requests;

public record CreateProductCommand(Guid UserId, CreateProductCommandDto CreateProductCommandDto) : IRequest<Guid>;

