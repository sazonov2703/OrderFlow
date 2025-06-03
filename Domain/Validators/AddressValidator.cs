using Domain.ValueObjects;
using FluentValidation;

namespace Domain.Validators;

public class ShippingAddressValidator : AbstractValidator<ShippingAddress>
{
    public ShippingAddressValidator()
    {
        
    }
}