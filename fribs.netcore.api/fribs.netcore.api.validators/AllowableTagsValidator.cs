using System.Collections.Generic;
using System.Linq;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Validators;

namespace fribs.netcore.api.validators
{
    public class AllowableTagsValidator : PropertyValidator
    {

        public AllowableTagsValidator() : base("'{PropertyName}' invalid. {ValidationMessage}") //note this message is never used
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null)
            {
                return true; //always allow nulls. If you dont want to allow null value, use NotEmpty() preceding this validator in the validation chain. 
            }

            var tags = (List<KeyValuePair<string,string>>)context.PropertyValue;

            //tag keys cannot be empty
            if (tags.Any(x => string.IsNullOrWhiteSpace(x.Key)))
            {
                context.MessageFormatter.AppendArgument("ValidationMessage", "'key' cannot be empty.");
                return false;
            }

            //tag keys cannot have dupes 
            if (!tags.GroupBy(x => x.Key.ToLower()).All(g => g.Count() == 1))
            {
                context.MessageFormatter.AppendArgument("ValidationMessage", "'key' must be unique in list.");
                return false;
            }

            return true;
        }

    }
    public static class MyTagsValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TElement> IsValidTags<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new AllowableTagsValidator());
        }
    }
}
