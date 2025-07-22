namespace Application.DTOs;

public record AddUserToWorkspaceByUsernameCommandDto
(
    Guid WorkspaceId, 
    string Username,
    string Role
);