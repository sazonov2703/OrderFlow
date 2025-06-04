using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Commands;

public class CreateCustomerCommandHandler(
    IWorkspaceReadRepository workspaceReadRepository,
    ICustomerWriteRepository customerWriteRepository
    ) : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var workspace = await workspaceReadRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);
        
        if (workspace == null) throw new InvalidOperationException($"Workspace {request.WorkspaceId} does not exist");
        
        var customer = new Customer(
            workspace, 
            request.FirstName, 
            request.LastName, 
            request.Patromymic, 
            request.Email, 
            request.PhoneNumbers,
            request.Links
            );
        
        await customerWriteRepository.AddAsync(customer, cancellationToken);
        
        await customerWriteRepository.SaveChangesAsync(cancellationToken);
        
        return customer.Id;
    }
}