
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ServiceStack;

namespace fribs.netcore.exceptions
{
	public class ExceptionMapper
	{
		public static Dictionary<Type, int> _statusCodes = null;

		public static Dictionary<Type, int> StatusCodes
		{
			get
			{
				if (_statusCodes == null)
				{
					_statusCodes = new Dictionary<Type, int>();

					foreach (Type extype in Assembly.GetExecutingAssembly().GetTypes().Where(extype => extype.GetInterfaces().Contains(typeof(IHasStatusCode))))
					{
						_statusCodes.Add(extype, ((IHasStatusCode)Activator.CreateInstance(extype)).StatusCode);
					}
				}

				return _statusCodes;
			}
		}		
	}
}
