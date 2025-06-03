using Domain.Events;
using Domain.Validators;

namespace Domain.Entities;

/// <summary>
/// Workspace
/// </summary>
public class Workspace : BaseEntity<Workspace>
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private Workspace()
    {
        
    }

    /// <summary>
    /// Public constructor
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="user">User</param>
    public Workspace(
        string name,
        User user
        )
    {
        Name = name;
        AddUser(user);
        
        // Validation
        ValidateEntity(new WorkspaceValidator());
        
        // Event throw
        AddDomainEvent(new WorkspaceCreatedEvent());
    }

    #endregion
    
    #region Properties
    
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; private set; }
    
    #region Navigation Properties
    
    /// <summary>
    /// Navigation property for linking to User
    /// </summary>
    public List<User> Users { get; private set; } = new List<User>();
    
    /// <summary>
    /// Navigation property for linking to Order
    /// </summary>
    public List<Order> Orders { get; private set; } = new List<Order>();
    
    #endregion
    
    #endregion
    
    #region Methods

    public void AddUser(User user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        if (Users.Any(u => u.Id == user.Id)) 
            throw new ArgumentException($"User {nameof(user)} already exists.");
        
        Users.Add(user);
        AddDomainEvent(new AddUserToWorkspaceEvent());
    }
    
    #endregion
}