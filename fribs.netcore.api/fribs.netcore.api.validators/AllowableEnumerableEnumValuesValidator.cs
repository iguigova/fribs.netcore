//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ServiceStack.FluentValidation.Validators;

//namespace fribs.netcore.api.validators
//{
//	public class AllowableEnumerableEnumValuesValidator<TEnum> : PropertyValidator where TEnum :  struct, IConvertible, IFormattable
//	{
//		private bool _allowNulls { get; set; }

//		public AllowableEnumerableEnumValuesValidator(bool allowNulls = true) : base($"Property has a value that is not allowed.")
//		{
//			_allowNulls = allowNulls;
//		}

//		protected override bool IsValid(PropertyValidatorContext context)
//		{
//			if (context.PropertyValue == null)
//			{
//				return _allowNulls;
//			}

//			if (context.PropertyValue.GetType() != typeof(IEnumerable<string>))
//			{
//				return false;
//			}

//			return (context.PropertyValue as List<string>).All(s => Enum.GetNames(typeof(TEnum)).Any(value => string.Compare(value.ToLower(), s, StringComparison.CurrentCultureIgnoreCase) == 0));
//		}
//	}
//}
