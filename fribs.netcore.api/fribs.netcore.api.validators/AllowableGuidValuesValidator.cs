using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Validators;

namespace fribs.netcore.api.validators
{
    public class AllowableGuidValuesValidator : PropertyValidator
    {

        public AllowableGuidValuesValidator() : base("'{PropertyName}' not found.") //note this message is never used
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return true; //always allow nulls. If you dont want to allow null value, use NotEmpty() preceding this validator in the validation chain. 
            }

			return false; // return context.PropertyValue.ToString().IsValidGuid();
        }
    }

    public static class MyGuidValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> IsValidGuid<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AllowableGuidValuesValidator());
        }
    }
}
