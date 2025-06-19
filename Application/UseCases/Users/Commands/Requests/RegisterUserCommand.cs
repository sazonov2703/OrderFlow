using MediatR;

namespace Application.UseCases.Users.Commands.Requests;

public record RegisterUserCommand(
    string Username,
    string Password,
    string Email
    ) : IRequest<string>;