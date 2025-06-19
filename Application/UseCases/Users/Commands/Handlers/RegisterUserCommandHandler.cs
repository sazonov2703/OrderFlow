using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Users.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Commands.Handlers;

public class RegisterUserCommandHandler(
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IJwtProvider  jwtProvider,
    IPasswordHasher passwordHasher
    )
    : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await userReadRepository.IsEmailExistAsync(request.Email, cancellationToken))
        {
            throw new ArgumentException($"User with email {request.Email} already exists");
        }

        if (await userReadRepository.IsUsernameExistAsync(request.Username, cancellationToken))
        {
            throw new ArgumentException($"User with username {request.Username} already exists");
        }

        var hashedPassword = passwordHasher.HashPassword(request.Password);

        var user = new User(request.Username, request.Email, hashedPassword);

        await userWriteRepository.AddAsync(user, cancellationToken);

        await userWriteRepository.SaveChangesAsync(cancellationToken);

        return await jwtProvider.GenerateToken(user, cancellationToken);
    }
}