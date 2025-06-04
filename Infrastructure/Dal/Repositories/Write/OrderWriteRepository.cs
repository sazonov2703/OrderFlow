using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class OrderWriteRepository(OrderFlowDbContext context) 
    : BaseWriteRepository<Order>(context), IOrderWriteRepository
{
    
}