using MediatR;

namespace Application.UseCases.Users.Queries.Requests;

public record LoginUserQuery(string Email, string Password) : IRequest<string>;