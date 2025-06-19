using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class OrderItemReadRepository : BaseReadRepository<OrderItem>, IOrderItemReadRepository
{
    private OrderFlowDbContext _context;
    public OrderItemReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}