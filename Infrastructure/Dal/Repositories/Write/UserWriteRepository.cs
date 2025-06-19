using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class UserWriteRepository : BaseWriteRepository<User>, IUserWriteRepository
{
    private OrderFlowDbContext _context;
    public UserWriteRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}