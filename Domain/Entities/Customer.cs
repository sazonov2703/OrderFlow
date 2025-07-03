using Domain.Events;
using Domain.Validators;

namespace Domain.Entities;

public class Customer : BaseEntity<Customer>
{
    #region Contructors
    
    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private Customer()
    {
        
    }

    /// <summary>
    /// Public constructor
    /// </summary>
    /// <param name="workspace">Workspace</param>
    /// <param name="firstName">First name</param>
    /// <param name="lastName">Last name</param>
    /// <param name="patronymic">Patronymic</param>
    /// <param name="email">Email</param>
    /// <param name="phoneNumber">Phone number</param>
    /// <param name="link">Link</param>
    public Customer(
        Workspace workspace,
        string? firstName,
        string? lastName,
        string? patronymic,
        string? email,
        string? phoneNumber,
        string? link
        )
    {
        Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));;
        WorkspaceId = workspace.Id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Email = email;
        PhoneNumber = phoneNumber;
        Link = link;
        
        // Validation
        ValidateEntity(new CustomerValidator());
        
        // Event throw
        AddDomainEvent(new CustomerCreatedEvent());
    }
    
    #endregion
    
    #region Properties

    /// <summary>
    /// First name
    /// </summary>
    public string? FirstName { get; private set; }
    
    /// <summary>
    /// Last name
    /// </summary>
    public string? LastName { get; private set; }
    
    /// <summary>
    /// Patronymic
    /// </summary>
    public string? Patronymic { get; private set; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; private set; }
    
    /// <summary>
    /// Phone number
    /// </summary>
    public string? PhoneNumber { get; private set; }
    
    /// <summary>
    /// Link to social media
    /// </summary>
    public string Link { get; private set; }

    #region Navigation Properties

    /// <summary>
    /// Navigation property for linking to Order
    /// </summary>
    public List<Order>? Orders { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Workspace
    /// </summary>
    public Workspace Workspace { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Workspace
    /// </summary>
    public Guid WorkspaceId { get; private set; }

    #endregion
    
    #endregion
    
    #region Methods


    
    #endregion
}