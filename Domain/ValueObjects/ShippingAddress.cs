using Domain.Validators;

namespace Domain.ValueObjects;

public class ShippingAddress : BaseValueObject
{
    #region Constructors
    
    
    public ShippingAddress(
        string recipentName, 
        string country, 
        string city, 
        string street,
        string houseNumber, 
        string flatNumber, 
        string zipCode
        )
    {
        RecipentName = recipentName;
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        FlatNumber = flatNumber;
        ZipCode = zipCode;
        
        // Validation
        ValidateValueObject(new ShippingAddressValidator());
    }
    
    #endregion
    
    #region Properties
    
    public string RecipentName { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string HouseNumber { get; private set; }
    public string FlatNumber { get; private set; }
    public string ZipCode { get; private set; }
    
    #endregion
}