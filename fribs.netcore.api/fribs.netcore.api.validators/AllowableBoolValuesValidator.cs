using ServiceStack.FluentValidation.Validators;

namespace fribs.netcore.api.validators
{
	public class AllowableBoolValuesValidator : PropertyValidator
	{
		public AllowableBoolValuesValidator() : base("'{PropertyName}' is not a valid boolean.")
		{
		}

		protected override bool IsValid(PropertyValidatorContext context)
		{
            if (context.PropertyValue == null)
            {
                return true;//always allow nulls. If you dont want to allow null value, use NotEmpty() preceding this validator in the validation chain. 
            }

            return string.Compare(context.PropertyValue.ToString(), "true", true) == 0 || string.Compare(context.PropertyValue.ToString(), "false", true) == 0;
		}
	}
}
