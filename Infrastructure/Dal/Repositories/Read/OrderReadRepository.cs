using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class OrderReadRepository : BaseReadRepository<Order>, IOrderReadRepository
{
    private OrderFlowDbContext _context;
    public OrderReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}