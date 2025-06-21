namespace Domain.Entities;

public class UserWorkspace
{
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
}