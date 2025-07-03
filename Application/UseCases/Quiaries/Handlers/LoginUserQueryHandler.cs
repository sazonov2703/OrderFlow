using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.UseCases.Quiaries.Requests;
using MediatR;

namespace Application.UseCases.Quiaries.Handlers;

public class LoginUserQueryHandler(
    IUserReadRepository userReadRepository,
    IJwtProvider  jwtProvider,
    IPasswordHasher passwordHasher
    ) : IRequestHandler<LoginUserQuery, string>
{
    public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var dto = request.LoginUserQueryDto;
        
        if (!await userReadRepository.IsEmailExistAsync(dto.Email, cancellationToken))
        {
            throw new KeyNotFoundException($"User with email {dto.Email} was not found");
        }
        
        var user = await userReadRepository.GetUserByEmailAsync(dto.Email, cancellationToken);

        if (!passwordHasher.VerifyHashedPassword(user.HashedPassword, dto.Password))
        {
            throw new ArgumentException($"Invalid password");
        }
        
        return await jwtProvider.GenerateToken(user, cancellationToken);
    }
}