using FluentValidation;
using NUnit.Framework;
using SUPEN20DB.Entites;


namespace SystemAPI.Validators
{
 
    public class SaveOrderItemsValidator : AbstractValidator<OrderItem>
    {
        public SaveOrderItemsValidator()
        {
            
        }
    }
}
