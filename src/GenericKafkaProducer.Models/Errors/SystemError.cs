namespace GenericKafkaProducer.Models.Errors
{
    using System;
    using Newtonsoft.Json;

    public class SystemError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ClassName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MethodName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int LineNumber { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception SystemException { get; set; }
    }
}