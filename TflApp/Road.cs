using System;
using System.Text.Json.Serialization;

namespace TflApp
{
    public abstract class Road
    {
        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("displayName")]
        public string displayName { get; set; }

        public abstract void WriteLine();

    }

    public class ValidRoad : Road
    {

        [JsonPropertyName("id")]
        public string id { get; set; }


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
        public Uri relativeUri { get; set; }

        public override void WriteLine()
        {
            Console.WriteLine($"{displayName} is not a valid road");
            Console.ReadKey();
        }
    }
}
