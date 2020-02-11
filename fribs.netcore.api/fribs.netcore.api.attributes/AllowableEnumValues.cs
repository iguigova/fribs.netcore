using System;

namespace fribs.netcore.api.attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class AllowableEnumValues : ServiceStack.ApiAllowableValuesAttribute
	{
		public AllowableEnumValues(Type enumType) : base(enumType.Name, enumType)
		{
			if (enumType.IsSubclassOf(typeof(Enum)))
			{
				Type = "LIST";
				Values = Enum.GetNames(enumType);
			}
		}
	}
}
