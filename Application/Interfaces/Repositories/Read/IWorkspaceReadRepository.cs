using Domain.Entities;

namespace Application.Interfaces.Repositories.Read;

public interface IWorkspaceReadRepository : IReadRepository<Workspace>
{
    Task<List<Workspace>> GetByUserAsync(Guid userId, CancellationToken cancellationToken);
}