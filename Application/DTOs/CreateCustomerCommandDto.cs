namespace Application.DTOs;

public record CreateCustomerCommandDto(
    Guid WorkspaceId, 
    string FirstName, 
    string LastName,
    string Patromymic, 
    string Email, 
    string? PhoneNumber,
    string? Link);