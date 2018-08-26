using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Compatability_Tool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
            {
                var environment = webHostBuilderContext.HostingEnvironment;
                string pathOfCommonSettingsFile = Path.Combine(environment.ContentRootPath, "..", "CommonSettings");
                configurationbuilder
                        .AddJsonFile("appSettings.json", optional: true)
                        .AddJsonFile(Path.Combine(pathOfCommonSettingsFile, "Commonsettings.json"), optional: true);

                configurationbuilder.AddEnvironmentVariables();
            })
                .UseStartup<Startup>()
                .Build();
    }
}
