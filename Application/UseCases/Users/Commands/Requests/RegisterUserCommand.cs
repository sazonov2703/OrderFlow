using Application.UseCases.Users.Commands.DTOs;
using MediatR;

namespace Application.UseCases.Users.Commands.Requests;

public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<string>;