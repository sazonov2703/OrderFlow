namespace Application.DTOs;

public record RegisterUserCommandDto(string Username, string Password, string Email);