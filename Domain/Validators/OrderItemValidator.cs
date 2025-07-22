using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator()
    {
        
    }
}