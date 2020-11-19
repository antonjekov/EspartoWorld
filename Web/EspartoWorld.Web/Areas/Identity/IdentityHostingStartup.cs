using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EspartoWorld.Web.Areas.Identity.IdentityHostingStartup))]

namespace EspartoWorld.Web.Areas.Identity
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
