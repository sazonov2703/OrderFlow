using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class UserReadRepository : BaseReadRepository<User>, IUserReadRepository
{
    private OrderFlowDbContext _context;
    public UserReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
                   .AsNoTracking()
                   .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken)
               ?? throw new KeyNotFoundException($"User with email '{email}' not found.");
    }

    public async Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
                   .AsNoTracking()
                   .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower(), cancellationToken)
               ?? throw new KeyNotFoundException($"User with username '{username}' not found.");
    }

    public async Task<bool> IsEmailExistAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .AnyAsync(u => u.Email.ToLower() == email.ToLower(), cancellationToken);
    }
    
    public async Task<bool> IsUsernameExistAsync(string username, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .AnyAsync(u => u.Username.ToLower() == username.ToLower(), cancellationToken);
    }
}