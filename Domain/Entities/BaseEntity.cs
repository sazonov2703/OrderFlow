using Domain.Interfaces;
using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Domain.Entities;

/// <summary>
/// Base class for all domain entities, providing ID comparison and validation
/// </summary>
public abstract class BaseEntity<T> where T : BaseEntity<T>
{
    /// <summary>
    /// Domain events list
    /// </summary>
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Default constructor
    /// </summary>
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Unique identificator
    /// </summary>
    public Guid Id { get; protected init; }

    #region Methods

    /// <summary>
    /// Performs validation on an entity using the specified validator
    /// </summary>
    /// <param name="validator">Валидатор FluentValidator.</param>
    protected void ValidateEntity(AbstractValidator<T> validator)
    {
        var validationResult = validator.Validate((T)this);
        if (validationResult.IsValid)
        {
            return;
        }

        var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
        throw new ValidationException(errorMessages);
    }

    #region System methods

    /// <summary>
    /// Overriding the Equals method to compare entities by ID
    /// </summary>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || obj.GetType() != GetType()) return false;

        return Id.Equals(((BaseEntity<T>)obj).Id);
    }

    /// <summary>
    /// Override the GetHashCode method to get a hash code based on a unique identifier
    /// </summary>
    /// <returns>A hash code based on the value of an identifier</returns>
    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Equality operator for comparing two BaseEntity instances by ID
    /// </summary>
    /// <param name="left">Left entity for comparison</param>
    /// <param name="right">Right entity for comparison</param>
    /// <returns>True if the entities are not equal; otherwise False</returns>
    public static bool operator ==(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    /// <summary>
    /// Inequality operator for comparing two BaseEntity instances by ID.
    /// </summary>
    /// <param name="left">Left entity for comparison</param>
    /// <param name="right">Right entity for comparison</param>
    /// <returns>True if the entities are not equal; otherwise False</returns>
    public static bool operator !=(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        return !(left == right);
    }

    #endregion

    #endregion

    #region Domain methods

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    #endregion
}