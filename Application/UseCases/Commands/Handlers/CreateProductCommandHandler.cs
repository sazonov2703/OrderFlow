using Application.Interfaces.Repositories.Write;
using Application.Services.Workspaces;
using Application.UseCases.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

public class CreateProductCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    IProductWriteRepository productWriteRepository
    ) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateProductCommandDto;
        
        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, dto.WorkspaceId, cancellationToken); ;

        var product = new Product(
            workspace, dto.Name, dto.Description, dto.UnitPrice, dto.ImageUrl);
        
        await productWriteRepository.AddAsync(product, cancellationToken);
        
        await productWriteRepository.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }
}