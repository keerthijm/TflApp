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

            string path = GetUrl(args, host);

            using (IServiceScope serviceScope = host.Services.CreateScope())
            {
                IServiceProvider services = serviceScope.ServiceProvider;

                try
                {
                    ApiService myService = services.GetRequiredService<ApiService>();
                    string result = await myService.GetAsync(path);
                    JToken token = JToken.Parse(result);
                    Road road = JsonTokenReaderUtility.ConvertToObject(token);
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
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
            IConfigurationSection app_id = config.GetSection(CommonConstants.APPID);
            IConfigurationSection app_key = config.GetSection(CommonConstants.APPKey);
            IConfigurationSection url = config.GetSection(CommonConstants.URL);
            string path = url.Value.Replace(CommonConstants.ARG1, args[0]).Replace(CommonConstants.ARG2, app_id.Value).Replace(CommonConstants.ARG3, app_key.Value);
            return path;
        }

    }
}
