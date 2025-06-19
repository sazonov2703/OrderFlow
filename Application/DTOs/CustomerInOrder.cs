namespace Application.DTOs;

public record CustomerInOrder(
    Guid? CustomerId,
    string firstName,
    string lastName,
    string patronymic,
    string email,
    List<string> phoneNumbers,
    List<string> links
    );