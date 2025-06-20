using Application.UseCases.Users.Commands.DTOs;
using MediatR;

namespace Application.UseCases.Users.Commands.Requests;

public record RegisterUserCommand(RegisterUserCommandDto RegisterUserCommandDto) : IRequest<string>;