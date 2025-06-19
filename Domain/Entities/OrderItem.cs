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
        Product product,
        Order? order,
        int quantity
        )
    {
        Product = product;
        ProductId = product.Id;
        Order = order;
        OrderId = order.Id;
        ProductName = product.Name;
        ProductUnitPrice = product.UnitPrice;
        Quantity = quantity;
        
        TotalPrice = ProductUnitPrice * Quantity;
        
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
    public decimal TotalPrice { get; private set; }
    
    #region Navigation Properties
    
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

    public void SetOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentException("Order cannot be empty.");
        };
        
        Order = order;
        OrderId = order.Id;
    }
    
    #endregion
}