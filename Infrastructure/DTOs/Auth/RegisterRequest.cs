namespace Infrastructure.DTOs.Auth;

public record RegisterRequest(string Username, string Email, string Password);