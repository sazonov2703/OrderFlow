using Application.Interfaces.Repositories.Read;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories.Read;

public class ProductReadRepository : BaseReadRepository<Product>, IProductReadRepository
{
    private OrderFlowDbContext _context;
    public ProductReadRepository(OrderFlowDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetByCompositeFieldsAsync(
        Guid workspaceId,
        string name,
        string description,
        decimal unitPrice,
        string? imageUrl,
        CancellationToken cancellationToken)
    {
        return await _context.Products
            .Include(p => p.Workspace)
            .FirstOrDefaultAsync(p =>
                    p.Workspace.Id == workspaceId &&
                    p.Name == name &&
                    p.Description == description &&
                    p.UnitPrice == unitPrice &&
                    p.ImageUrl == imageUrl,
                cancellationToken);
    }
}