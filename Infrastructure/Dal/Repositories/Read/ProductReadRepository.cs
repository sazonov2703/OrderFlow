using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class ProductReadRepository : BaseReadRepository<Product>, IProductReadRepository
{
    private OrderFlowDbContext _context;
    public ProductReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}