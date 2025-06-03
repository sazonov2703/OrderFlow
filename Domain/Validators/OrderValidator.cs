using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        
    }
}