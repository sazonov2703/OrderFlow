using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Application.Services.Customers;

public class CustomerResolverService(
    ICustomerReadRepository customerReadRepository,
    ICustomerWriteRepository customerWriteRepository)
{
    public async Task<Customer> GetOrCreateCustomer(
        Workspace workspace, Guid? customerId, string? firstName, string? lastName, string? patronymic,
        string? email, List<string>? phoneNumbers, List<string>? links, CancellationToken cancellationToken)
    {
        Customer customer;
        if (customerId is not null || customerId != Guid.Empty)
        {
            if (customerId != null)
                customer = await customerReadRepository.GetByIdAsync(customerId.Value, cancellationToken);
            else
            {
                throw new NullReferenceException("Customer id is null");
            }
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