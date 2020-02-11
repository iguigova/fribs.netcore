using System;
using System.Linq;
using System.Reflection;
using ServiceStack;

namespace fribs.netcore.api.authorizers
{
	public static class AuthorizerMapper<T>
	{
		public static IAuthorizer<T> _authorizer = null;

		public static IAuthorizer<T> Authorizer
		{
			get
			{
				if (_authorizer == null)
				{
					_authorizer = (IAuthorizer<T>)HostContext.Metadata.Operations.Where(s => s.RequestType == typeof(T))
						.SelectMany(s => s.ServiceType.GetMethods()).Where(s => s.GetParameters().Any(p => p.ParameterType == typeof(T)))
						.SelectMany(s => s.GetCustomAttributes())
						.First(s => s.GetType().GetInterfaces().Contains(typeof(IAuthorizer<T>)));
				}

				return _authorizer;
			}
		}

		//public static Dictionary<Type, IAuthorizer<T>> _athorizers = null;

		//public static Dictionary<Type, IAuthorizer<T>> Authorizers
		//{
		//	get
		//	{
		//		if (_athorizers == null)
		//		{
		//			var serviceType = HostContext.Metadata.Operations.First(o => o.RequestType == typeof(T)).ServiceType;

		//			foreach (var methodInfo in serviceType.GetMethods())
		//			{
		//				foreach (var parameter in methodInfo.GetParameters())
		//				{
		//					if (parameter.ParameterType == typeof(T))
		//					{
		//						_athorizers.Add(typeof(T), (IAuthorizer<T>)methodInfo.GetCustomAttributes().First(s => s.GetType().GetInterfaces().Contains(typeof(IAuthorizer<T>))));
		//					}
		//				}
		//			}
		//		}

		//		return _athorizers;
		//	}
		//}
	}
}
