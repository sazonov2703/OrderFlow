namespace Domain.Entities;

public class OrderItem : BaseEntity<OrderItem>
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private OrderItem()
    {
        
    }

    public OrderItem(
        Workspace workspace, 
        Product? product,
        Order order,
        int quantity
    )
    {
        Workspace = workspace;
        WorkspaceId = workspace.Id;
        
        Order = order;
        OrderId = order.Id;
        
        if (product is not null)
        {
            UpdateProduct(product, quantity);;
        }
        
        //ValidateEntity();
        //AddDomainEvent();
    }
    
    #endregion
    
    #region Properties

    /// <summary>
    /// Product name
    /// </summary>
    public string ProductName { get; private set; }

    /// <summary>
    /// Product unit price
    /// </summary>
    public decimal ProductUnitPrice { get; private set; }

    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; private set; }
    
    /// <summary>
    /// Total price
    /// </summary>
    public decimal TotalPrice {
        get
        {
            return ProductUnitPrice * Quantity;
        }
        private set
        {
            
        }
    }
    
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
    /// Navigation property for linking to Order
    /// </summary>
    public Order Order { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Order
    /// </summary>
    public Guid OrderId { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Product
    /// </summary>
    public Product Product { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Product
    /// </summary>
    public Guid ProductId { get; private set; }

    #endregion
    
    #endregion
    
    #region Methods
    
    /// <summary>
    /// Method for setting or updating Product of OrderItem.
    /// </summary>
    /// <param name="product">Product</param>
    /// <param name="quantity">Quantity</param>
    /// <exception cref="ArgumentException">Product cannot be empty.</exception>
    public void UpdateProduct(Product product, int quantity)
    {
        if (product == null)
        {
            throw new ArgumentException("Product cannot be empty.");
        };
        
        Product = product;
        ProductId = product.Id;
            
        ProductName = product.Name ?? string.Empty;
        ProductUnitPrice = product.UnitPrice;
        Quantity = quantity;
        
        TotalPrice = ProductUnitPrice * Quantity;
        
        //ValidateEntity();
        //AddDomainEvent();
    }

    /// <summary>
    /// Method for updating fields of OrderItem without changing the Product.
    /// </summary>
    /// <param name="productName">Product name</param>
    /// <param name="productUnitPrice">Product unit price</param>
    /// <param name="quantity">Quantity</param>
    public void UpdateFields(string? productName, decimal? productUnitPrice, int? quantity)
    {
        if (productName is null && productUnitPrice is null && quantity is null)
        {
            throw new ArgumentException("At least one field must be updated.");
        }
        
        if (productName is not null)
        {
            ProductName = productName;
        }
        
        if (productUnitPrice is not null)
        {
            ProductUnitPrice = (decimal)productUnitPrice;
        }
        
        if (quantity is not null)
        {
            Quantity = (int)quantity;
        }
        
        if (productUnitPrice is not null || quantity is not null)
        {
            TotalPrice = ProductUnitPrice * Quantity;
        }
        
        //ValidateEntity();
        //AddDomainEvent();
    }

    public void SetProduct(Product product)
    {
        if (product is null)
        {
            throw new ArgumentException("Product cannot be empty.");
        }
        
        Product = product;
        ProductId = product.Id;
        
        //ValidateEntity();
        //AddDomainEvent();
    }
    
    #endregion
}