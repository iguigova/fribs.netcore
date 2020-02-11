using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using ServiceStack;
using ServiceStack.Text;

namespace fribs.netcore.api.extensions
{
	public static class Context
	{
		public static IHttpContextAccessor Accessor { get; set; }

		public static HttpContext Current { get { return Accessor?.HttpContext; } }

		private static Version _version;
		public static Version Version { get { return _version ?? (_version = Assembly.GetExecutingAssembly().GetName().Version); } }

		public static string Id
		{
			get
			{
				if (Current != null)
				{
					if (!(Current.Items["Id"] is string id))
					{
						Current.Items["Id"] = id = Guid.NewGuid().ToString();
					}

					return id;
				}

				return null;
			}
		}

		public static List<string> Logs
		{
			get
			{
				if (Current != null)
				{
					if (!(Current.Items["Logs"] is List<string> logs))
					{
						Current.Items["Logs"] = logs = new List<string>();
					}

					return logs;
				}

				return null;
			}
		}

		public static void Configure()
		{
			//we dont want to do this. See here: https://docs.microsoft.com/en-us/dotnet/framework/network-programming/tls
			//System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

			// https://servicestack.net/pricing
			//			Licensing.RegisterLicense(@"");

			JsConfig.DateHandler = DateHandler.ISO8601;
			JsConfig.AssumeUtc = true;
			JsConfig.AppendUtcOffset = false;

			JsConfig.TextCase = TextCase.SnakeCase;

			JsConfig.ThrowOnError = true;
			JsConfig.ExcludeTypeInfo = true;
			JsConfig.IncludeTypeInfo = false;

			//crazy as it is.. apparantly some of the deserialization config needs to be done BEFORE app init. 
			//See https://forums.servicestack.net/t/jsconfig-deserializefn-rawdeserializefn-not-called-for-parameters-in-querystring/1192/6

			JsConfig<bool>.DeSerializeFn = s =>
			{
				if (string.IsNullOrWhiteSpace(s))
				{
					throw new SerializationException();
				}

				s = s.ToLower();
				if (s.CompareTo("true") == 0)
				{
					return true;
				}
				else if (s.CompareTo("false") == 0)
				{
					return false;
				}
				throw new SerializationException();
			};

			JsConfig<bool?>.DeSerializeFn = s =>
			{
				if (string.IsNullOrWhiteSpace(s))
				{
					return null;
				}

				s = s.ToLower();
				if (s.CompareTo("true") == 0)
				{
					return true;
				}
				else if (s.CompareTo("false") == 0)
				{
					return false;
				}
				throw new SerializationException();
			};

			JsConfig<int>.DeSerializeFn = s =>
			{
				if (string.IsNullOrWhiteSpace(s))
				{
					throw new SerializationException();
				}

				if (int.TryParse(s, out int tmp))
				{
					return tmp;
				}

				throw new SerializationException();
			};

			JsConfig<int?>.DeSerializeFn = s =>
			{
				if (string.IsNullOrWhiteSpace(s))
				{
					return null;
				}

				if (int.TryParse(s, out int tmp))
				{
					return tmp;
				}

				throw new SerializationException();
			};

			JsConfig<DateTime>.SerializeFn = dt => dt.ToUniversalTime().ToString("O", System.Globalization.CultureInfo.InvariantCulture);

			JsConfig<DateTime?>.SerializeFn = dt => dt.HasValue ? dt.Value.ToUniversalTime().ToString("O", System.Globalization.CultureInfo.InvariantCulture) : null;

			//JsConfig<Guid>.SerializeFn = guid => guid.ToString("N"); //no hyphens

			//JsConfig<Guid?>.SerializeFn = guid => guid.HasValue ? guid.Value.ToString("N") : null; //no hyphens
		}
	}
}
