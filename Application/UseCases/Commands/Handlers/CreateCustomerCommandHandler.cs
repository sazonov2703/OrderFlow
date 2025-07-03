using Application.Interfaces.Repositories.Write;
using Application.Services.Workspaces;
using Application.UseCases.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands.Handlers;

public class CreateCustomerCommandHandler(
    WorkspaceAccessService workspaceAccessService,
    ICustomerWriteRepository customerWriteRepository
    ) : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateCustomerCommandDto;

        var workspace = await workspaceAccessService.GetAndValidateWorkspaceById(
            request.UserId, dto.WorkspaceId, cancellationToken);
        
        if (workspace == null) throw new InvalidOperationException($"Workspace {dto.WorkspaceId} does not exist");
        
        var customer = new Customer(
            workspace, 
            dto.FirstName, 
            dto.LastName, 
            dto.Patromymic, 
            dto.Email, 
            dto.PhoneNumber,
            dto.Link
            );
        
        await customerWriteRepository.AddAsync(customer, cancellationToken);
        
        await customerWriteRepository.SaveChangesAsync(cancellationToken);
        
        return customer.Id;
    }
}