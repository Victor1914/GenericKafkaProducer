namespace GenericKafkaProducer.Models.Responses
{
    using Errors;
    using Newtonsoft.Json;

    public class ProducerResponse : ProducerBaseResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public dynamic Data { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public SystemError SystemError { get; set; }
    }
}