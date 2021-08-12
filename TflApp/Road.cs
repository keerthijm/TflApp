using System;
using System.Text.Json.Serialization;

namespace TflApp
{
    /// <summary>
    /// Road as Base Class and Valid and Invalid Road as Derived Class
    /// </summary>
    public abstract class Road
    {
        [JsonPropertyName("type")]
        public string type { get; set; }       
        public abstract void WriteLine();

    }
    /// <summary>
    /// Valid Road has information on road id, display name, status information, Derived from Road base class
    /// </summary>
    public class ValidRoad : Road
    {
        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("displayName")]
        public string displayName { get; set; }

        [JsonPropertyName("statusSeverity")]
        public string statusSeverity { get; set; }

        [JsonPropertyName("statusSeverityDescription")]
        public string statusSeverityDescription { get; set; }

        public override void WriteLine()
        {
            Console.WriteLine($"The status of the {displayName} is as follows");
            Console.WriteLine($"Road Status is {statusSeverity}");
            Console.WriteLine($"Road Status Description is {statusSeverityDescription}");
            Console.WriteLine($"The status of the {displayName} is as follows");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Invalid road has httpStatus and httpStatusCode inforamtion and Derived from Road base class 
    /// </summary>
    public class InvalidRoad : Road
    {
        [JsonPropertyName("timestampUtc")]
        public string timestampUtc { get; set; }

        [JsonPropertyName("exceptionType")]
        public string exceptionType { get; set; }

        [JsonPropertyName("httpStatusCode")]
        public string httpStatusCode { get; set; }

        [JsonPropertyName("httpStatus")]
        public string httpStatus { get; set; }

        [JsonPropertyName("relativeUri")]
        public string relativeUri { get; set; }
        public override void WriteLine()
        {
            var roadname = relativeUri.Split("/")[2];
            Console.WriteLine($"{roadname} is not a valid road");
            Console.ReadKey();
        }
    }
}
