using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Application.Services.Products;

public class ProductAccessService(
    IProductReadRepository productReadRepository)
{
    public async Task<Product> GetAndValidateProductByIdAsync(
        Guid productId, Guid workspaceId, CancellationToken cancellationToken)
    {
        var product = await productReadRepository.GetByIdAsync(productId, cancellationToken);

        if (product == null)
        {
            throw new InvalidOperationException($"Product {productId} not found");
        }

        if (product.WorkspaceId != workspaceId)
        {
            throw new UnauthorizedAccessException($"Product {productId} not belong to workspace {workspaceId}");
        }

        return product;
    }
}