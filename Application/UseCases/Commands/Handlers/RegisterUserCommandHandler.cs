using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

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
        var dto = request.RegisterUserCommandDto;
        
        if (await userReadRepository.IsEmailExistAsync(dto.Email, cancellationToken))
        {
            throw new ArgumentException($"User with email {dto.Email} already exists");
        }

        if (await userReadRepository.IsUsernameExistAsync(dto.Username, cancellationToken))
        {
            throw new ArgumentException($"User with username {dto.Username} already exists");
        }

        var hashedPassword = passwordHasher.HashPassword(dto.Password);

        var user = new User(dto.Username, dto.Email, hashedPassword);

        var token = await jwtProvider.GenerateToken(user, cancellationToken);
        
        await userWriteRepository.AddAsync(user, cancellationToken);

        await userWriteRepository.SaveChangesAsync(cancellationToken);

        return token;
    }
}