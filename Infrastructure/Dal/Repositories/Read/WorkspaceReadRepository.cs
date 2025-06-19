using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class WorkspaceReadRepository(OrderFlowDbContext context) 
    : BaseReadRepository<Workspace>(context), IWorkspaceReadRepository
{
    public async Task<List<Workspace>> GetByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Include(u => u.Workspaces)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
            throw new KeyNotFoundException($"User with id {userId} not found");

        return user.Workspaces.ToList();
    }
}