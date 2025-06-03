using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        
    }
}