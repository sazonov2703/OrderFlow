using Domain.Entities;

namespace Application.Interfaces;

public interface IJwtProvider
{
    Task<string> GenerateToken(User user, CancellationToken cancellationToken);
}