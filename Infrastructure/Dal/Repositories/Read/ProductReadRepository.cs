using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class ProductReadRepository(OrderFlowDbContext context) : BaseReadRepository<Product>(context), IProductReadRepository
{
    
}