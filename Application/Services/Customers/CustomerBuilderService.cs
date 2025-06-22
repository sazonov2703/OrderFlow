using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Application.Services.Customers;

public class CustomerBuilderService(
    ICustomerWriteRepository customerWriteRepository,
    CustomerAccessService customerAccessService)
{
    public async Task<Customer> GetOrCreateCustomerAsync(
        Workspace workspace, 
        Guid? customerId, 
        string? firstName, 
        string? lastName, 
        string? patronymic,
        string? email, 
        List<string>? phoneNumbers, 
        List<string>? links, 
        CancellationToken cancellationToken)
    {
        Customer customer;
        if (customerId is not null && customerId != Guid.Empty)
        {
            customer = await customerAccessService.GetAndValidateCustomerByIdAsync(
                customerId.Value, workspace.Id, cancellationToken);
        }
        else
        {
            customer = new Customer(
                workspace, firstName, lastName, patronymic, email, phoneNumbers, links);
            
            await customerWriteRepository.AddAsync(customer, cancellationToken);
        }
        
        return customer;
    }
}