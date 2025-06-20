using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Products.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Products.Commands.Handlers;

public class CreateProductCommandHandler(
    IProductWriteRepository productWriteRepository, 
    IWorkspaceReadRepository workspaceReadRepository
    ) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var workspace = await workspaceReadRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);
        
        if (workspace == null) throw new InvalidOperationException($"Workspace {request.WorkspaceId} does not exist");

        var product = new Product(
            workspace, request.Name, request.Description, request.UnitPrice, request.ImageUrl);
        
        await productWriteRepository.AddAsync(product, cancellationToken);
        
        await productWriteRepository.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }
}