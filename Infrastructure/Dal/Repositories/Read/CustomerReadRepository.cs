using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class CustomerReadRepository : BaseReadRepository<Customer>, ICustomerReadRepository
{
    private OrderFlowDbContext _context;
    public CustomerReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}