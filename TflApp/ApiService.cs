using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TflApp
{
    /// <summary>
    /// ApiService class to include methods which can coommunitcate to API's, like GET, POST
    /// </summary>
    public class ApiService
    {
        private readonly ILogger _logger;
        private IHttpClientFactory _httpFactory { get; set; }

        public ApiService(ILogger<ApiService> logger, IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _httpFactory = httpFactory;
        }
        /// <summary>
        /// Get data from api
        /// </summary>
        /// <param name="url">url as string - path for accessing get</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url)
        {
            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Started", DateTime.UtcNow);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            HttpClient client = _httpFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            _logger.LogInformation("Application {applicationEvent} at {dateTime}", "Ended", DateTime.UtcNow);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
