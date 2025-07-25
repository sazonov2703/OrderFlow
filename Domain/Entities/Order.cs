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
    public Order(
        Workspace workspace,
        List<OrderItem>? orderItems,
        Customer? customer,
        ShippingAddress? shippingAddress,
        decimal? shippingCost,
        string? description
    )
    {
        Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        WorkspaceId = workspace.Id;

        if (customer is not null)
        {
            Customer = customer;
            CustomerId = customer.Id;
        }

        OrderItems = new();
        if (orderItems != null)
        {
            foreach (var orderItem in orderItems)
            {
                AddOrderItem(orderItem);
            }
        }
        
        ShippingAddress = shippingAddress ?? null;
        ShippingCost = shippingCost ?? 0;
        Description = description ?? string.Empty;

        Status = Status.Pending;
        
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
    public decimal TotalAmount
    {
        get { return OrderItems.Sum(oi => oi.TotalPrice) + ShippingCost; }
        private set { } 
    }

    /// <summary>
    /// Shipping cost
    /// </summary>
    public decimal ShippingCost { get; private set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; private set; }
    
    /// <summary>
    /// Status
    /// </summary>
    public Status Status { get; private set; }

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
    
    public void AddOrderItem(OrderItem orderItem)
    {
        if (orderItem is null)
        {
            throw new ArgumentNullException(nameof(orderItem));
        }
        
        OrderItems.Add(orderItem);
        
        //ValidateEntity();
    }
    
    public void ClearOrderItems()
    {
        OrderItems.Clear();
        
        //ValidateEntity();
    }

    public void Update(
        Customer? customer = null,
        ShippingAddress? shippingAddress = null,
        decimal? shippingCost = null,
        string? description = null)
    {
        if (customer != null)
            Customer = customer;
    
        if (shippingAddress != null)
            ShippingAddress = shippingAddress;
    
        if (shippingCost.HasValue)
            ShippingCost = shippingCost.Value;
    
        if (!string.IsNullOrEmpty(description))
            Description = description;
        
        //ValidateEntity();
        //AddDomainEvent();
    }

    public void UpdateStatus(Status status)
    {
        Status = status;
        
        //ValidateEntity();
        //AddDomainEvent();
    }
    #endregion
}