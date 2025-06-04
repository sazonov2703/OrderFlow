using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class OrderItemWriteRepository(OrderFlowDbContext context) 
    : BaseWriteRepository<OrderItem>(context), IOrderItemWriteRepository
{
    
}