using FluentValidation;
using NUnit.Framework;
using SUPEN20DB.Entites;


namespace SystemAPI.Validators
{
 
    public class SaveOrderItemsValidator : AbstractValidator<OrderItem>
    {
        public SaveOrderItemsValidator()
        {
            RuleFor(item => item.Total).NotEmpty().WithMessage("The total cannot be empty or 0");
            //RuleFor(item => item.Quantity).NotEmpty().WithMessage("The Quantity cannot be empty or 0"); 
        }
    }
}
