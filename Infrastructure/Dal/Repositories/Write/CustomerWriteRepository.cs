using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class CustomerWriteRepository : BaseWriteRepository<Customer>, ICustomerWriteRepository
{
    private OrderFlowDbContext _context;
    public CustomerWriteRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}