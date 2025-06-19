using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class UserReadRepository(OrderFlowDbContext context)
    : BaseReadRepository<User>(context), IUserReadRepository
{
    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Set<User>()
                   .AsNoTracking()
                   .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower())
               ?? throw new KeyNotFoundException($"User with email '{email}' not found.");
    }

    public async Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await context.Set<User>()
                   .AsNoTracking()
                   .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower())
               ?? throw new KeyNotFoundException($"User with username '{username}' not found.");
    }

    public async Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .AnyAsync(u => u.Email.ToLower() == email.ToLower());
    }
    
    public async Task<bool> IsUsernameExistAsync(string username, CancellationToken cancellationToken)
    {
        return await context.Set<User>()
            .AnyAsync(u => u.Username.ToLower() == username.ToLower());
    }
}