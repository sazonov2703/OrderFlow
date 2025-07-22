namespace Application.DTOs;

public record DeleteUserFromWorkspaceByUsernameCommandDto(Guid WorkspaceId, string Username);