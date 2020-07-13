using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DotNetEnv;

namespace ResearchTube
{
    public class Program
    {
        private const string StripeWebrootKey = "STATIC_DIR";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            DotNetEnv.Env.Load();
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var webRoot = Environment.GetEnvironmentVariable(StripeWebrootKey);

                    webBuilder.UseStartup<Startup>();

                   // webBuilder.UseWebRoot(webRoot);
                });
        }
            
    }
}
