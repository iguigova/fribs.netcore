using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Validators;

namespace fribs.netcore.api.validators
{
    public class AllowableDateTimeValuesValidator : PropertyValidator
    {

        public AllowableDateTimeValuesValidator() : base("'{PropertyName}' is not a valid date.")//note this message is never used
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
			if (context.PropertyValue == null)
			{
				return true; //always allow nulls. If you dont want to allow null value, use NotEmpty() preceding this validator in the validation chain. 
			}

			return false;

			//if (context.PropertyValue.ToString().IsValidDateString())
			//{
			//    return true;
			//}
			//else
			//{
			//    return false;
			//}
		}
    }

    public static class MyDateTimeValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> IsValidDate<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AllowableDateTimeValuesValidator());
        }       
    }
}
