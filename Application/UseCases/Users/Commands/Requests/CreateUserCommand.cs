using MediatR;

namespace Application.UseCases.Users.Commands.Requests;

public record CreateUserCommand(
    string Username, 
    string Email, 
    string Password
    ) : IRequest<Guid>;