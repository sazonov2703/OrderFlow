using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.UseCases.Users.Queries.Requests;
using MediatR;

namespace Application.UseCases.Users.Queries.Handlers;

public class LoginUserQueryHandler(
    IUserReadRepository userReadRepository,
    IJwtProvider  jwtProvider,
    IPasswordHasher passwordHasher
    ) : IRequestHandler<LoginUserQuery, string>
{
    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        if (!await userReadRepository.IsEmailExistAsync(request.Email, cancellationToken))
        {
            throw new KeyNotFoundException($"User with email {request.Email} was not found");
        }
        
        var user = await userReadRepository.GetUserByEmailAsync(request.Email, cancellationToken);

        if (!passwordHasher.VerifyHashedPassword(user.HashedPassword, request.Password))
        {
            throw new ArgumentException($"Invalid password");
        }
        
        return await jwtProvider.GenerateToken(user, cancellationToken);
    }
}