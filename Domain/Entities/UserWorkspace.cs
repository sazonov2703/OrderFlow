namespace Domain.Entities;

public class UserWorkspace
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private UserWorkspace()
    {
        
    }

    public UserWorkspace(User user, Workspace workspace)
    {
        User = user;
        UserId = user.Id;
        Workspace = workspace;
        WorkspaceId = workspace.Id;
    }

    #endregion
    
    #region Properties
    
    #region Navigation Properties
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid WorkspaceId { get; set; }
    public Workspace Workspace { get; set; }
    
    #endregion
    
    #endregion
    
    #region Methods
    
    
    
    #endregion
}