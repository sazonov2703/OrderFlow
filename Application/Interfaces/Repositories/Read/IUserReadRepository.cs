using Domain.Entities;

namespace Application.Interfaces.Repositories.Read;

public interface IUserReadRepository : IReadRepository<User>
{
    Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
    Task<bool> IsUsernameExistAsync(string username, CancellationToken cancellationToken);
    Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken);
}