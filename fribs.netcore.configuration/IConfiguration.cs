using System.Collections.Generic;
using fribs.netcore.cache;

namespace fribs.netcore.configuration
{
	public interface IConfiguration
	{
		T Get<T>(string name, T defaultValue = default(T), char separator = ',', bool isRequired = false);

		string ApplicationEnvironment { get; }

		string ApplicationName { get; }

		//string DataDbConnString { get; }

		string RedisXConnString { get; }

		ICache Cache { get; }

		string PapertrailUrl { get;}

		int PapertrailPort { get;}

		List<string> CorsWhitelist { get; }
    }
}
