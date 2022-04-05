namespace GenericKafkaProducer.Models.Responses
{
    using Interfaces;

    public class ProducerBaseResponse : IProducerResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}