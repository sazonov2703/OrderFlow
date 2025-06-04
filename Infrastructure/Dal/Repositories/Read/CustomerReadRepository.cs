using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class CustomerReadRepository(OrderFlowDbContext context) 
    : BaseReadRepository<Customer>(context), ICustomerReadRepository
{
    
}