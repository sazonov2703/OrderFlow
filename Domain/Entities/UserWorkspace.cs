using Domain.ValueObjects;

namespace Domain.Entities;

public class UserWorkspace : BaseEntity<UserWorkspace>
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private UserWorkspace()
    {
        
    }

    public UserWorkspace(User user, Workspace workspace, WorkspaceRole? role)
    {
        User = user;
        UserId = user.Id;
        Workspace = workspace;
        WorkspaceId = workspace.Id;
        Role = role ?? WorkspaceRole.Member;
    }

    #endregion
    
    #region Properties
    
    public WorkspaceRole Role { get; set; }
    
    #region Navigation Properties
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    
    #endregion
    
    #endregion
    
    #region Methods
    
    public void UpdateRole(WorkspaceRole newRole)
    {
        Role = newRole;
        
        // ValidateEntity();
        // AddDomainEvent();
    }
    
    #endregion
}