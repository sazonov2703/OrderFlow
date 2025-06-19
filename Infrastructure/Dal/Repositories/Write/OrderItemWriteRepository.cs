using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class OrderItemWriteRepository : BaseWriteRepository<OrderItem>, IOrderItemWriteRepository
{
    private OrderFlowDbContext _context;
    public OrderItemWriteRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}