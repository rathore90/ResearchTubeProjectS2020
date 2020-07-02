using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResearchTube.Areas.Identity.Data;
using ResearchTube.Data;

[assembly: HostingStartup(typeof(ResearchTube.Areas.Identity.IdentityHostingStartup))]
namespace ResearchTube.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ResearchTubeDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ResearchTubeDbContextConnection")));

                services.AddDefaultIdentity<ResearchTubeUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                })
                    .AddEntityFrameworkStores<ResearchTubeDbContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}