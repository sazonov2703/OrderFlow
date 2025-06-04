using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class OrderItemReadRepository(OrderFlowDbContext context) 
    : BaseReadRepository<OrderItem>(context), IOrderItemReadRepository
{
    
}