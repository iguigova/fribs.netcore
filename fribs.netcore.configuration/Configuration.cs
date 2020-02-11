using System;
using System.Collections.Generic;
using fribs.netcore.cache;

namespace fribs.netcore.configuration
{
	public class Configuration : IConfiguration
	{
		//private static IConfiguration _instance;
		public static IConfiguration Instance { get; private set; }

        public static void Init(Microsoft.Extensions.Configuration.IConfiguration netCoreConfig)
        {
            Instance = new Configuration(netCoreConfig);
        }

        //private readonly Dictionary<string, string> _dbConfigurationSettings;
        Microsoft.Extensions.Configuration.IConfiguration _netCoreConfig { get; set; }

        public Configuration(Microsoft.Extensions.Configuration.IConfiguration netCoreConfig)
		{
            // NOTE: This IData instance has ICache = null and is going to throw an exception anytime a query that uses cache is called. 
            // It MUST be used ONLY to query the ConfigurationSettings table because it is not cached
            //_dbConfigurationSettings = new Dictionary<string, string>(); // new DataImplementer(DataDbConnectionString).ConfigurationSettings_Get();
            _netCoreConfig = netCoreConfig;
        }

		private string Get(string name)
		{
			//return ConfigurationManager.AppSettings[name] ?? ((_dbConfigurationSettings.ContainsKey(name)) ? _dbConfigurationSettings[name] : null);
			return _netCoreConfig[name];
		}

		public T Get<T>(string name, T defaultValue = default(T), char separator = ',', bool isRequired = false)
		{
			var value = Get(name);

			if (!string.IsNullOrEmpty(value))
			{
				if (typeof(T) == typeof(string[]))
				{
					return (T)Convert.ChangeType(value.Split(separator), typeof(T));
				}

				if (typeof(T) == typeof(List<string>))
				{
					return (T)Convert.ChangeType(new List<string>(value.Split(separator)), typeof(T));
				}

				return (T)Convert.ChangeType(value, typeof(T));
			}

			if (isRequired)
			{
				throw new Exception($"Setting {name} is not defined.");
			}

			return defaultValue;
		}

		private string _applicationName;
		public string ApplicationName { get { return _applicationName ?? (_applicationName = Get<string>("ApplicationName", isRequired: true)); } }

		private string _applicationEnvironment;
		public string ApplicationEnvironment { get { return _applicationEnvironment ?? (_applicationEnvironment = Get<string>("ApplicationEnvironment", isRequired: true)); } }

		private string _redisXConnString;
		public string RedisXConnString { get { return _redisXConnString ?? (_redisXConnString = Get<string>("RedisXConnStr", isRequired: true)); } }

		private ICache _cache;
		public ICache Cache
		{
			get
			{
				if (_cache == null)
				{
					return new Cache(RedisXConnString);
				}

				return _cache;
			}
		}

		private string _papertrailUrl;
        public string PapertrailUrl => _papertrailUrl ?? (_papertrailUrl = Get<string>("PapertrailUrl", isRequired: false));

        private int? _papertrailPort;
        public int PapertrailPort { get { return (_papertrailPort ?? (_papertrailPort = Get<int>("PapertrailPort", -1))).Value; }}

	    private List<string> _corsWhitelist;
	    public List<string> CorsWhitelist { get { return _corsWhitelist ?? (_corsWhitelist = Get<List<string>>("CorsWhitelist", isRequired: false)); } }
    }
}

 