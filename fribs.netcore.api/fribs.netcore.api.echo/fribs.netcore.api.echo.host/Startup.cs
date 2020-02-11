using fribs.netcore.api.extensions;
using fribs.netcore.logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;

namespace fribs.netcore.api.echo.host
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            Context.Accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();

			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			applicationLifetime.ApplicationStopping.Register(Log.LogManager.Shutdown);

            app.UseServiceStack(new EchoAppHost($"Echo {Context.Version}"));
		}
    }
}
