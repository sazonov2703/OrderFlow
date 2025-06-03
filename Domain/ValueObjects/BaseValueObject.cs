using System.Reflection;
using FluentValidation;

namespace Domain.ValueObjects;

/// <summary>
/// Base class for all value objects, providing comparison and hash code calculation based on all fields and properties
/// </summary>
public abstract class BaseValueObject : IEquatable<BaseValueObject>
{
    /// <summary>
    /// Determines whether the current object is equal to the value of another object
    /// </summary>
    /// <param name="other">Another object for comparison</param>
    /// <returns>True if the objects are equal; otherwise False</returns>
    public bool Equals(BaseValueObject? other)
    {
        if (other == null || other.GetType() != GetType())
            return false;

        // Compare all properties
        foreach (var property in GetProperties())
        {
            var value1 = property.GetValue(this);
            var value2 = property.GetValue(other);
            if (!Equals(value1, value2))
                return false;
        }

        // Compare all fields
        foreach (var field in GetFields())
        {
            var value1 = field.GetValue(this);
            var value2 = field.GetValue(other);
            if (!Equals(value1, value2))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Overriding the Equals method to compare objects
    /// </summary>
    /// <param name="obj">Object for comparison</param>
    /// <returns>True if the objects are equal; otherwise False</returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as BaseValueObject);
    }

    /// <summary>
    /// Override the GetHashCode method to calculate the hash code based on all fields and properties
    /// </summary>
    /// <returns>Hash code of the object</returns>
    public override int GetHashCode()
    {
        int hash = 17;

        foreach (var property in GetProperties())
        {
            var value = property.GetValue(this);
            hash = hash * 31 + (value?.GetHashCode() ?? 0);
        }

        foreach (var field in GetFields())
        {
            var value = field.GetValue(this);
            hash = hash * 31 + (value?.GetHashCode() ?? 0);
        }

        return hash;
    }

    /// <summary>
    /// Gets a list of all available properties of an object for comparison
    /// </summary>
    /// <returns>A collection of object properties</returns>
    private IEnumerable<PropertyInfo> GetProperties()
    {
        return GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !p.GetGetMethod()!.IsVirtual);
    }

    /// <summary>
    /// Gets a list of all available fields of the object for comparison
    /// </summary>
    /// <returns>A collection of object fields</returns>
    private IEnumerable<FieldInfo> GetFields()
    {
        return GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
    }

    /// <summary>
    /// Equality comparison operator for value objects
    /// </summary>
    /// <param name="left">Left object for comparison</param>
    /// <param name="right">Right object for comparison</param>
    /// <returns>True if the objects are equal; otherwise False</returns>
    public static bool operator ==(BaseValueObject? left, BaseValueObject? right)
    {
        return left?.Equals(right) ?? ReferenceEquals(right, null);
    }

    /// <summary>
    /// Inequality comparison operator for value objects
    /// </summary>
    /// <param name="left">Left object for comparison</param>
    /// <param name="right">Right object for comparison</param>
    /// <returns>True if the objects are not equal; otherwise False</returns>
    public static bool operator !=(BaseValueObject? left, BaseValueObject? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Method for performing validation on a value object using the specified validator
    /// </summary>
    /// <typeparam name="T">The type of the value object</typeparam>
    /// <param name="validator">Validator for verification</param>
    /// <exception cref="ValidationException">Exception if validation fails</exception>
    protected void ValidateValueObject<T>(IValidator<T> validator) where T : BaseValueObject
    {
        var result = validator.Validate((T)this);
        if (!result.IsValid)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validation failed for {typeof(T).Name}: {errors}");
        }
    }
}