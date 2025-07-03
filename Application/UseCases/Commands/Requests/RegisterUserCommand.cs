using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands.Requests;

public record RegisterUserCommand(RegisterUserCommandDto RegisterUserCommandDto) : IRequest<string>;