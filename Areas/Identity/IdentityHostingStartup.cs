using Microsoft.AspNetCore.Hosting;


[assembly: HostingStartup(typeof(RaceApp.Areas.Identity.IdentityHostingStartup))]
namespace RaceApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}