using Domain.Validators;

namespace Domain.ValueObjects;

public class ShippingAddress : BaseValueObject
{
    #region Constructors
    
    public ShippingAddress(
        string? recipentName, 
        string? country, 
        string? city, 
        string? street,
        string? houseNumber, 
        string? flatNumber, 
        string? zipCode
        )
    {
        RecipentName = recipentName ?? string.Empty;
        Country = country ?? string.Empty;
        City = city ?? string.Empty;
        Street = street ?? string.Empty;
        HouseNumber = houseNumber ?? string.Empty;
        FlatNumber = flatNumber ?? string.Empty;
        ZipCode = zipCode ?? string.Empty;
        
        // Validation
        ValidateValueObject(new ShippingAddressValidator());
    }
    
    #endregion
    
    #region Properties
    
    public string? RecipentName { get; private set; }
    public string? Country { get; private set; }
    public string? City { get; private set; }
    public string? Street { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? FlatNumber { get; private set; }
    public string? ZipCode { get; private set; }
    
    #endregion
}