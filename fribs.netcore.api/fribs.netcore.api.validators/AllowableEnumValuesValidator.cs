using ServiceStack.FluentValidation.Validators;

namespace fribs.netcore.api.validators
{
    public class AllowableEnumValuesValidator<TEnum> : PropertyValidator where TEnum : struct
    {

        public AllowableEnumValuesValidator() : base("'{PropertyName}' has a value that is not allowed.")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return true; //always allow nulls. If you dont want to allow null value, use NotEmpty() preceding this validator in the validation chain. 
            }

			return false; // return context.PropertyValue.ToString().IsValidEnumString<TEnum>();
        }
    }


}
