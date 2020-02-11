using System;

namespace fribs.netcore.api.attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class AllowableBoolValues : ServiceStack.ApiAllowableValuesAttribute
	{
		public AllowableBoolValues() : base("bool")
		{
			Values = new string[] { "true", "false" };
		}
	}
}
