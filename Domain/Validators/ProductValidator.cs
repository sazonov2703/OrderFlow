using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        
    }
}