using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class OrderWriteRepository : BaseWriteRepository<Order>, IOrderWriteRepository
{
    private OrderFlowDbContext _context;
    public OrderWriteRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}