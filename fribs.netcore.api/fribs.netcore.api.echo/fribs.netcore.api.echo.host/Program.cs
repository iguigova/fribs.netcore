using fribs.netcore.configuration;
using fribs.netcore.api.extensions;
using fribs.netcore.logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using System;

namespace fribs.netcore.api.echo.host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureAppConfiguration((configBuilder) =>
				{
					Context.Configure(); //before apphost init

					configBuilder.ApplyAppSettingsFromFile();

					//specify the Env Vars source last, as the last source always wins - https://blogs.msdn.microsoft.com/cjaliaga/2016/08/10/working-with-azure-app-services-application-settings-and-connection-strings-in-asp-net-core/
					configBuilder.AddEnvironmentVariables(); // get it via env. vars (what the app service transforms AppSettings into) 

					Configuration.Init(configBuilder.Build());								

					AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
					{
						Log.Error(eventArgs.ExceptionObject as Exception);
					};
				})
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
