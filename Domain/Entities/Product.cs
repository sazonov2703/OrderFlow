using Domain.Events;
using Domain.Validators;

namespace Domain.Entities;

public class Product : BaseEntity<Product>
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private Product()
    {
        
    }

    /// <summary>
    /// Public constructor
    /// </summary>
    /// <param name="workspace">Workspace</param>
    /// <param name="name">Nme</param>
    /// <param name="description">Description</param>
    /// <param name="unitPrice">Price</param>
    public Product(
        Workspace workspace,
        string name, 
        string description, 
        decimal unitPrice
        )
    {
        Workspace = workspace;
        WorkspaceId = workspace.Id;
        Name = name;
        Description = description;
        UnitPrice = unitPrice;
        
        // Validation
        ValidateEntity(new ProductValidator());
        
        // Event throw
        AddDomainEvent(new ProductCreatedEvent());
    }
    #endregion
    
    #region Properties

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Price
    /// </summary>
    public decimal UnitPrice { get; private set; }
    
    #region Navigation Properties

    /// <summary>
    /// Navigation property for linking to Order
    /// </summary>
    public List<Order> Orders { get; private set; } = new List<Order>();
    
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