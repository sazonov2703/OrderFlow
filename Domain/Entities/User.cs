using Domain.Events;
using Domain.Validators;

namespace Domain.Entities;

/// <summary>
/// User
/// </summary>
public class User : BaseEntity<User>
{
    #region Constructors
    
    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private User()
    {
        
    }

    /// <summary>
    /// Public constructor
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="email">Email</param>
    /// <param name="hashedPassword">Encrypted password</param>
    public User(
        string username, 
        string email, 
        string hashedPassword)
    {
        Username = username;
        Email = email;
        HashedPassword = hashedPassword;
        
        // Validation
        ValidateEntity(new UserValidator());
        
        // Event throw
        AddDomainEvent(new UserCreatedEvent());
    }
    
    #endregion
    
    #region Properties
    
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; private set; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; private set; }
    
    /// <summary>
    /// Encrypted password
    /// </summary>
    public string HashedPassword { get; private set; }
    
    #region Navigation Properties
    
    /// <summary>
    /// Navigation property for linking to UserWorkspace
    /// </summary>
    public List<UserWorkspace> UserWorkspaces { get; private set; } = new();
    
    /// <summary>
    /// Navigation property for linking to ExternalLogin
    /// </summary>
    public List<ExternalLogin> ExternalLogins { get; private set; }
    
    #endregion
    
    #endregion
    
    #region Methods
    
    
    
    #endregion
}