namespace Application.UseCases.Users.Commands.DTOs;

public record RegisterUserCommandDto(string Username, string Password, string Email);