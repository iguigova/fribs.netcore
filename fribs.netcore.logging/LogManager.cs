namespace fribs.netcore.logging
{
    public class LogManager
    {
		public NLog.Logger Logger { get; private set; }

		public LogManager(string papertrailUrl, int papertrailPort)
		{
			NLog.LogManager.Configuration = new NLog.Config.LoggingConfiguration();

			var target = (NLog.Targets.Target)new NLog.Targets.ConsoleTarget() 
			{ 
				Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}" 
			};

			if (!string.IsNullOrEmpty(papertrailUrl))
			{
				target = new NLog.Targets.Syslog.SyslogTarget()
				{
					//Enforcement = new NLog.Targets.Syslog.Settings.EnforcementConfig()
					//{
					//    Throttling = new NLog.Targets.Syslog.Settings.ThrottlingConfig()
					//    {
					//        Strategy = NLog.Targets.Syslog.Settings.ThrottlingStrategy.Block
					//    }
					//},

					MessageCreation = new NLog.Targets.Syslog.Settings.MessageBuilderConfig()
					{
						Facility = NLog.Targets.Syslog.Settings.Facility.Local7
					},

					MessageSend = new NLog.Targets.Syslog.Settings.MessageTransmitterConfig()
					{
						Protocol = NLog.Targets.Syslog.Settings.ProtocolType.Tcp,
						Tcp = new NLog.Targets.Syslog.Settings.TcpConfig()
						{

							Server = papertrailUrl,
							Port = papertrailPort,
							Tls = new NLog.Targets.Syslog.Settings.TlsConfig()
							{
								Enabled = true
							}

						},
					},

					Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
				};
			}

			NLog.LogManager.Configuration.AddRuleForAllLevels(target);

			Logger = NLog.LogManager.GetCurrentClassLogger();

			Logger.Info<string>("Start logging...");
		}

        public void Shutdown()
        {
            Logger.Info<string>("Shutdown logging...");

            NLog.LogManager.Shutdown();
        }
    }
}
