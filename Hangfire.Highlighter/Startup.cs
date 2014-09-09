using Hangfire.Highlighter;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Hangfire.Highlighter
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseHangfire(config =>
            {
                var options = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                };

                config.UseSqlServerStorage("HighlighterDb", options);
                config.UseServer();
            });

            app.MapSignalR();
        }
    }
}