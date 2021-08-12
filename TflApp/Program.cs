using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace TflApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Using AppSettings for URL, app_id and app_key
            IHostBuilder builder = new HostBuilder()
                 .ConfigureAppConfiguration((hostContext, builder) =>
                 {
                     builder.AddJsonFile(CommonConstants.APPSETTINGSFILE);
                     builder.AddEnvironmentVariables();
                     if (args != null)
                     {
                         builder.AddCommandLine(args);
                     }
                 })
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddHttpClient();
                  services.AddTransient<ApiService>();
              }).UseConsoleLifetime();

            IHost host = builder.Build();

            // Create URL based on command line arguments and reading from appsettings file
            string path = GetUrl(args, host);

            using (IServiceScope serviceScope = host.Services.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;

                try
                {
                    // ApiService object to access get method
                    ApiService myService = services.GetRequiredService<ApiService>();
                    string result = await myService.GetAsync(path);

                    // Parse result as token, so we can check if it is array or a sinlge object
                    JToken token = JToken.Parse(result);

                    // Pass json token to conver to Valid or Invalid Road object
                    var road = JsonTokenReaderUtility.ConvertToObject(token);

                    // Print the output
                    road.WriteLine();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error Occured", exception.Message);
                }
            }

        }
        private static string GetUrl(string[] args, IHost host)
        {
            var config = host.Services.GetRequiredService<IConfiguration>();
            var app_id = config.GetSection(CommonConstants.APPID);
            var app_key = config.GetSection(CommonConstants.APPKey);
            var url = config.GetSection(CommonConstants.URL);
            var path = url.Value.Replace(CommonConstants.ARG1, args[0]).Replace(CommonConstants.ARG2, app_id.Value).Replace(CommonConstants.ARG3, app_key.Value);
            return path;
        }

    }
}
