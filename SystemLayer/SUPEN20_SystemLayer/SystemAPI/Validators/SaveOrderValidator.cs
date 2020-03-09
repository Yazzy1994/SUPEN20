using FluentValidation;
using SUPEN20DB.Entites;

namespace SystemAPI.Validators
{

    public class SaveOrderValidator : AbstractValidator<Order> 
    {

        public SaveOrderValidator() //These are rules for order entity attributes.
        {
            RuleFor(o => o.OrderId).NotNull().WithMessage("The order Id is required");
            RuleFor(o => o.OrderNumber).NotEmpty().WithMessage("The order Ordernumber can't be empty");
            RuleFor(o => o.Created).NotEmpty();
            RuleFor(o => o.LastModified).NotEmpty();
            RuleFor(o => o.Total).NotEmpty().WithMessage("The total cannot be empty or 0");
            //RuleFor(item => item.Quantity).NotEmpty().WithMessage("The Quantity cannot be empty or 0");
        }
    }
}
