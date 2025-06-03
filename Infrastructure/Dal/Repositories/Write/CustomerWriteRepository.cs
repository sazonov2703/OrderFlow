using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class CustomerWriteRepository(OrderFlowDbContext context) : BaseWriteRepository<Customer>(context), ICustomerWriteRepository
{
    
}