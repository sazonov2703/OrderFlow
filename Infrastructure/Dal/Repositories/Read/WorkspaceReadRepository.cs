using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class WorkspaceReadRepository: BaseReadRepository<Workspace>, IWorkspaceReadRepository
{
    private readonly OrderFlowDbContext _context;
    public WorkspaceReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Workspace>> GetByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Workspaces)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with id {userId} not found");

        return user.Workspaces.ToList();
    }
}