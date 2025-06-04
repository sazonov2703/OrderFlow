using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class ProductWriteRepository(OrderFlowDbContext context) 
    : BaseWriteRepository<Product>(context), IProductWriteRepository
{
    
}