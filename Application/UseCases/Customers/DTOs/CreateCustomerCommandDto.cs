namespace Application.UseCases.Customers.DTOs;

public record CreateCustomerCommandDto(
    Guid WorkspaceId, 
    string FirstName, 
    string LastName,
    string Patromymic, 
    string Email, 
    List<string> PhoneNumbers,
    List<string> Links);