using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Application.Services.Products;

public class ProductBuilderService(
    IProductReadRepository productReadRepository,
    IProductWriteRepository productWriteRepository,
    ProductAccessService productAccessService)
{
    public async Task<Product> GetOrCreateProduct(
        Workspace workspace, 
        Guid? productId, 
        string? name, 
        string? description,
        decimal? unitPrice, 
        string? imageUrl, 
        CancellationToken cancellationToken)
    {
        if (productId is { } id && id != Guid.Empty)
        {
            return await productAccessService.GetAndValidateProductByIdAsync(id, workspace.Id, cancellationToken);
        }
        
        var existingProduct = await productReadRepository.GetByCompositeFieldsAsync(
            workspace.Id, 
            name ?? string.Empty, 
            description ?? string.Empty, 
            unitPrice ?? 0, 
            imageUrl ?? string.Empty, 
            cancellationToken);

        if (existingProduct != null)
        {
            return existingProduct;
        }
        
        var product = new Product(
            workspace, 
            name ?? string.Empty, 
            description ?? string.Empty, 
            unitPrice ?? 0, 
            imageUrl ?? string.Empty);

        await productWriteRepository.AddAsync(product, cancellationToken);
        
        return product;
    }
}