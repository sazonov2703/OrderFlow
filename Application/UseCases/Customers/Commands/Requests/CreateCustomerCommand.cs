using MediatR;

namespace Application.UseCases.Customers.Commands.Requests;

public record CreateCustomerCommand(
    Guid WorkspaceId, 
    string FirstName, 
    string LastName,
    string Patromymic, 
    string Email, 
    List<string> PhoneNumbers,
    List<string> Links
    ) : IRequest<Guid>;
