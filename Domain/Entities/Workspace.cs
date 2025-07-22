using Domain.Events;
using Domain.Validators;
using Domain.ValueObjects;

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
        AddUser(user, WorkspaceRole.Owner);
        
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
    /// Navigation property for linking to UserWorkspace
    /// </summary>
    public List<UserWorkspace> UserWorkspaces { get; private set; } = new();
    
    /// <summary>
    /// Navigation property for linking to Order
    /// </summary>
    public List<Order> Orders { get; private set; } = new List<Order>();
    
    /// <summary>
    /// Navigation property for linking to Customer
    /// </summary>
    public List<Customer> Customers { get; private set; } = new List<Customer>(); 
    
    /// <summary>
    /// Navigation property for linking to Product
    /// </summary>
    public List<Product> Products { get; private set; } = new List<Product>();
    
    #endregion
    
    #endregion
    
    #region Methods

    public void AddUser(User user, WorkspaceRole? role)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));

        if (UserWorkspaces.Any(uw => uw.UserId == user.Id))
            throw new ArgumentException($"User {nameof(user)} already exists.");
        
        UserWorkspaces.Add(new UserWorkspace(user, this, role));
        
        // ValidateEntity();
        AddDomainEvent(new AddUserToWorkspaceEvent());
    }
    
    public void RemoveUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        var userWorkspace = UserWorkspaces.FirstOrDefault(uw => uw.UserId == user.Id);
        
        if (userWorkspace is null)
        {
            throw new ArgumentException($"User {nameof(user)} does not exist.");
        }
        
        UserWorkspaces.Remove(userWorkspace);
        
        AddDomainEvent(new UserRemovedFromWorkspaceEvent());
    }
    
    #endregion
}