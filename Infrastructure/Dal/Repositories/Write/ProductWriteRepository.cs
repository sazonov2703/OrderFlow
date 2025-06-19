using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class ProductWriteRepository : BaseWriteRepository<Product>, IProductWriteRepository
{
    private OrderFlowDbContext _context;
    public ProductWriteRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
}