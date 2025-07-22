using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class ExternalLoginValidator : AbstractValidator<ExternalLogin>
{
    public ExternalLoginValidator()
    {
        
    }
}