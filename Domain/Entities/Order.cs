using Domain.Events;
using Domain.Validators;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Order
/// </summary>
public class Order : BaseEntity<Order>
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private Order()
    {
        
    }

    /// <summary>
    /// Public constructor
    /// </summary>
    /// <param name="workspace">Workspace</param>
    /// <param name="orderItems">Order items</param>
    /// <param name="customer">Customer</param>
    /// <param name="shippingAddress">Shipping address</param>
    /// <param name="shippingCost">Shipping cost</param>
    /// <param name="description">Description</param>
    /// <param name="deadline">Close date</param>
    public Order(
        Workspace workspace, 
        List<OrderItem> orderItems,
        Customer? customer,
        ShippingAddress? shippingAddress,
        decimal? shippingCost,
        string description,
        DateTime closeDate
        )
    {
        Workspace = workspace;
        WorkspaceId = workspace.Id;
        
        if (customer is not null)
        {
            Customer = customer;
            CustomerId = customer.Id;
        }
        
        OrderItems = orderItems;
        ShippingAddress = shippingAddress ?? null;
        ShippingCost = shippingCost ?? 0;
        Description = description;
        
        foreach (var orderItem in orderItems)
        {
            TotalAmount += orderItem.TotalPrice;
        }
        
        
        TotalAmount += shippingCost ?? 0;

        SetOrderDeadline(closeDate);
        
        // Validation
        ValidateEntity(new OrderValidator());
        
        // Event throw
        AddDomainEvent(new OrderCreatedEvent());
    }
    #endregion
    
    #region Properties

    /// <summary>
    /// Total deal value
    /// </summary>
    public decimal TotalAmount { get; private set; } = 0;
    
    /// <summary>
    /// Shipping cost
    /// </summary>
    public decimal ShippingCost { get; private set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// CloseDate
    /// </summary>
    public DateTime CloseDate { get; private set; }
    
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
    /// Navigation property for linking to Customer
    /// </summary>
    public Customer? Customer { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Customer
    /// </summary>
    public Guid? CustomerId { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to OrderItem
    /// </summary>
    public List<OrderItem> OrderItems { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to ShippingAddress
    /// </summary>
    public ShippingAddress? ShippingAddress { get; private set; }

    #endregion

    #endregion
    
    #region Methods
    
    public void SetOrderDeadline(DateTime closeDate)
    {
        if (closeDate < DateTime.UtcNow) throw new ArgumentException("Order date cannot be earlier than today.");
        
        CloseDate = closeDate;
    }
    
    #endregion
}