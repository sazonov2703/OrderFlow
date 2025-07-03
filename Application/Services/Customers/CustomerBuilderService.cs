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
        string? phoneNumber, 
        string? link, 
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
                workspace, firstName, lastName, patronymic, email, phoneNumber, link);
            
            await customerWriteRepository.AddAsync(customer, cancellationToken);
        }
        
        return customer;
    }
}