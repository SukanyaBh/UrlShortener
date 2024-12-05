using Cloud5mins.ShortenerTools.Core.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Cloud5mins.ShortenerTools
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication()
                .ConfigureAppConfiguration((context, config) =>
                {
                    // Load local.settings.json if running locally
                    config.SetBasePath(Directory.GetCurrentDirectory())  // Ensure it's using the correct base path
                          .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);  // Load the local settings file
                })
                .ConfigureServices((context, services) =>
                {

                    var configuration = context.Configuration;
                    var shortenerSettings = new ShortenerSettings();

                    // Bind the settings
                    configuration.Bind("ShortenerSettings", shortenerSettings);  // Bind the ShortenerSettings section

                    // Register the settings as a singleton
                    services.AddSingleton(shortenerSettings);
                })
                .Build();

            host.Run();
        }
    }
}
