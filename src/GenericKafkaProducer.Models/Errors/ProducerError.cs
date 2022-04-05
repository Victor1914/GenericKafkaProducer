namespace GenericKafkaProducer.Models.Errors
{
    public class ProducerError
    {
        public string Topic { get; set; }

        public string FileName { get; set; }
    }
}