using Application.UseCases.Users.Queries.DTOs;
using MediatR;

namespace Application.UseCases.Users.Queries.Requests;

public record LoginUserQuery(LoginUserQueryDto LoginUserQueryDto) : IRequest<string>;