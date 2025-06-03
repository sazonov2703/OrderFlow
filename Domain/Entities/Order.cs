using System.Security.Cryptography;
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
    /// <param name="customer">Customer</param>
    /// <param name="orderItems">Order items</param>
    /// <param name="shippingAddress">Shipping address</param>
    /// <param name="totaAmount">Total amount</param>
    public Order(
        Workspace workspace, 
        Customer customer, 
        List<OrderItem> orderItems,
        ShippingAddress shippingAddress,
        decimal totaAmount)
    {
        Workspace = workspace;
        WorkspaceId = workspace.Id;
        Customer = customer;
        CustomerId = customer.Id;
        OrderItems = orderItems;
        ShippingAddress = shippingAddress;
        TotalAmount = totaAmount;

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
    public decimal TotalAmount { get; private set; }
    
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
    public Customer Customer { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to Customer
    /// </summary>
    public Guid CustomerId { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to OrderItem
    /// </summary>
    public List<OrderItem> OrderItems { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to ShippingAddress
    /// </summary>
    public ShippingAddress ShippingAddress { get; private set; }

    #endregion

    #endregion
    
    #region Methods
    
    
    
    #endregion
}