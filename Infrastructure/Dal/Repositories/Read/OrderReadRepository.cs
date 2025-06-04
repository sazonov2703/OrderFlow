using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class OrderReadRepository(OrderFlowDbContext context) 
    : BaseReadRepository<Order>(context), IOrderReadRepository
{
    
}