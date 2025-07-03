using Application.DTOs;
using MediatR;

namespace Application.UseCases.Quiaries.Requests;

public record LoginUserQuery(LoginUserQueryDto LoginUserQueryDto) : IRequest<string>;