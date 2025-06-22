using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Application.Services.Customers;

public class CustomerAccessService(
    ICustomerReadRepository customerReadRepository)
{
    public async Task<Customer> GetAndValidateCustomerByIdAsync(
        Guid customerId, Guid workspaceId, CancellationToken cancellationToken)
    {
        var customer = await customerReadRepository.GetByIdAsync(customerId, cancellationToken);

        if (customer == null)
        {
            throw new InvalidOperationException($"Customer {customerId} not found");
        }
        
        if (customer.WorkspaceId != workspaceId)
        {
            throw new InvalidOperationException($"Customer {customerId} not found in workspace {workspaceId}");
        }
        
        return customer;
    }
}