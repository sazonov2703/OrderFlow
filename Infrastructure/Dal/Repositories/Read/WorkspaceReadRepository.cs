using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class WorkspaceReadRepository(OrderFlowDbContext context) : BaseReadRepository<Workspace>(context), IWorkspaceReadRepository
{
    
}