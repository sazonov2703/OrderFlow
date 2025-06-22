using Domain.Entities;

namespace Application.Interfaces.Repositories.Read;

public interface IProductReadRepository : IReadRepository<Product>
{
    Task<Product?> GetByCompositeFieldsAsync(
        Guid workspaceId,
        string? name,
        string? description,
        decimal unitPrice,
        string? imageUrl,
        CancellationToken cancellationToken);
}