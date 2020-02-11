using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Validators;

namespace fribs.netcore.api.validators
{
    public class AllowableIpAddressValidator : PropertyValidator
    {
        public AllowableIpAddressValidator() : base("'{PropertyName}' not a valid IP Address.") //note this message is never used
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return true; //always allow nulls. If you dont want to allow null value, use NotEmpty() preceding this validator in the validation chain. 
            }

			return false; // return context.PropertyValue.ToString().IsValidIpAddress();
        }
    }

    public static class MyIpAddressValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> IsValidIpAddress<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AllowableIpAddressValidator());
        }
    }
}
