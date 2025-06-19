using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class WorkspaceWriteRepository : BaseWriteRepository<Workspace>, IWorkspaceWriteRepository
{
    private OrderFlowDbContext _context;
    public WorkspaceWriteRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}