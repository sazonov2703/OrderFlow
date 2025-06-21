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
        string description
    )
    {
        Workspace = workspace;
        WorkspaceId = workspace.Id;

        if (customer is not null)
        {
            Customer = customer;
            CustomerId = customer.Id;
        }

        OrderItems = orderItems ?? new List<OrderItem>();
        ShippingAddress = shippingAddress ?? null;
        ShippingCost = shippingCost ?? 0;
        Description = description;
        
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
    }

    /// <summary>
    /// Shipping cost
    /// </summary>
    public decimal ShippingCost { get; private set; }
    
    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; private set; }
    
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
    
    public void UpdateCustomer(Customer customer)
    {
        if (customer is null)
        {
            throw new ArgumentException("Customer cannot be empty.");
        };
        
        Customer = customer;
        CustomerId = customer.Id;
        
        //ValidateEntity();
    }

    public void ChangeStatus(Status status)
    {
        if (status is null)
        {
            throw new ArgumentException("Status cannot be empty.");
        };
        
        Status = status;
        
        //ValidateEntity();
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        if (orderItem is null)
        {
            throw new ArgumentNullException(nameof(orderItem));
        }
        
        OrderItems.Add(orderItem);
        
        //ValidateEntity();
    }
    
    public void RemoveOrderItem(OrderItem orderItem)
    {
        if (orderItem is null)
        {
            throw new ArgumentNullException(nameof(orderItem));
        }
        
        OrderItems.Remove(orderItem);
    }
    
    #endregion
}