using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Contrib.HttpClient;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TflApp;

namespace TestTflApp
{
    [TestClass]
    public class GetAsyncAndParseTests
    {
        private static string GetUrl(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IConfigurationSection app_id = configuration.GetSection(CommonConstants.APPID);
            IConfigurationSection app_key = configuration.GetSection(CommonConstants.APPKey);
            IConfigurationSection url = configuration.GetSection(CommonConstants.URL);
            string path = url.Value.Replace(CommonConstants.ARG1, args[0]).Replace(CommonConstants.ARG2, app_id.Value).Replace(CommonConstants.ARG3, app_key.Value);
            return path;
        }

        [TestMethod]
        public void ShouldReturnBasicRequests()
        {
            // Arrange
            string Content = "{'test':test}";
            Mock<ILogger<ApiService>> logger = new Mock<ILogger<ApiService>>();
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Content),
                });

            // Create an HttpClient to send requests to the TestServer
            HttpClient client = new HttpClient(mockHttpMessageHandler.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            ApiService apiService = new ApiService(logger.Object, mockFactory.Object);
            string[] args = new string[1] { "A2" };
            string url = GetUrl(args);
            Task<string> result = apiService.GetAsync(url);
            Assert.AreEqual(Content, result.Result);
        }

        [TestMethod]
        public void Should_respond_with_basic_requests()
        {
            string[] args = new string[1] { "A2" };
            string url = GetUrl(args);
            Mock<ILogger<ApiService>> logger = new Mock<ILogger<ApiService>>();
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            HttpClient client = mockHttpMessageHandler.CreateClient();
            mockHttpMessageHandler.SetupRequest(HttpMethod.Get, url).ReturnsResponse("stuff");
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            ApiService apiService = new ApiService(logger.Object, mockFactory.Object);
            string result = apiService.GetAsync(url).Result;
        }



        [TestMethod]
        public void Should_respond_with_basic_requests2()
        {
            string[] args = new string[1] { "A2" };
            string url = GetUrl(args);
            Mock<ILogger<ApiService>> logger = new Mock<ILogger<ApiService>>();
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            HttpClient client = mockHttpMessageHandler.CreateClient();

            mockHttpMessageHandler.SetupRequest(HttpMethod.Get, url).ReturnsResponse(HttpStatusCode.Created);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            ApiService apiService = new ApiService(logger.Object, mockFactory.Object);
            Task<string> result = apiService.GetAsync(url);

        }

        [TestMethod]
        public void ShouldJsonArrayParseCorrectly()
        {
            string jsonArray = "[{ \"type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a2\", \"displayName\":\"A2\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\", \"bounds\":\"[[-0.0857,51.44091],[0.17118,51.49438]]\",\"envelope\":\"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\",\"url\":\"/Road/a2\" }]";
            JToken token = JToken.Parse(jsonArray);
            Road road = JsonTokenReaderUtility.ConvertToObject(token);
            Assert.AreEqual("A2", road.displayName);
            Assert.AreEqual("Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities", road.type);
        }


        [TestMethod]
        public void ShouldJsonObjectParseCorrectly()
        {
            string jsonObject = "[{ \"type\":\"Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities\",\"timestampUtc\":\"2017-11-21T14:37:39.7206118Z\", \"exceptionType\":\"EntityNotFoundException\",\"httpStatusCode\":\"404\",\"httpStatus\":\"NotFound\", \"relativeUri\":\"/Road/A233\",\"message\":\"The following road id is not recognised: A233\" }]";
            JToken token = JToken.Parse(jsonObject);
            Road road = JsonTokenReaderUtility.ConvertToObject(token);
            Assert.AreEqual("A233", road.displayName);
            Assert.AreEqual("Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities", road.type);
        }

    }
}
