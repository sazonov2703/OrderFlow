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
    /// <param name="unitPrice">Price</param>
    /// <param name="imageUrl">Image URL</param>
    /// <param name="description">Description</param>
    public Product(
        Workspace workspace,
        string? name,
        string? description,
        decimal? unitPrice,
        string? imageUrl
        )
    {
        Workspace = workspace;
        WorkspaceId = workspace.Id;
        
        Name = name ?? string.Empty;
        UnitPrice = unitPrice ?? 0;
        Description = description ?? string.Empty;
        ImageUrl = imageUrl ?? string.Empty;;
        
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
    public string? Name { get; private set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; private set; }
    
    /// <summary>
    /// Price
    /// </summary>
    public decimal UnitPrice { get; private set; }
    
    /// <summary>
    /// Image URL
    /// </summary>
    public string? ImageUrl { get; private set; }
    
    #region Navigation Properties

    /// <summary>
    /// Navigation property for linking to Workspace
    /// </summary>
    public Workspace Workspace { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Workspace
    /// </summary>
    public Guid WorkspaceId { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to OrderItems
    /// </summary>
    public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    #endregion
    
    #endregion
    
    #region Methods

    
    
    #endregion
}