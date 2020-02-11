using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace fribs.netcore.api.extensions
{
	public static class Extensions
	{
		public static void AddCors(this HttpResponse res, string origin, List<string> corsWhitelist)
		{
			if (!string.IsNullOrEmpty(origin) && corsWhitelist != null && corsWhitelist.Contains(origin))
			{
				res.Headers.Add("Vary", "Origin"); //need to specify this since it will change based on which origin is calling - https://developer.mozilla.org/en-US/docs/Web/HTTP/Access_control_CORS (on page search for "also include Origin in the Vary response header")
				res.Headers.Add("Access-Control-Allow-Origin", origin); //return allow header that is exactly what was on the requests Origin header
				res.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, GET, DELETE, OPTIONS");
				res.Headers.Add("Access-Control-Allow-Credentials", "true");
			}
		}

		public static void ApplyAppSettingsFromFile(this IConfigurationBuilder configBuilder)
		{
			//an interesting little thing. The purpose of this is to allow us to host our on prem, and local dev settings in one spot. We still need to make sure that this is compat. with the app service way of configuring settings in .net core. 

			var baseDir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location);

			var appSettingsFile = "appsettings.json";

			while (!File.Exists(Path.Combine(baseDir.FullName, appSettingsFile)))
			{
				if (baseDir.Parent != null && Directory.Exists(baseDir.Parent.FullName))
				{
					baseDir = new DirectoryInfo(baseDir.Parent.FullName);
				}
				else
				{
					break; //if it doesnt exist.. we exit and assume its Azure hosted
				}
			}

			appSettingsFile = Path.Combine(baseDir.FullName, appSettingsFile);

			if (File.Exists(appSettingsFile))
			{
				configBuilder.AddJsonFile(appSettingsFile, optional: false); //for dev / on prem deploys
			}
		}
	}
}
