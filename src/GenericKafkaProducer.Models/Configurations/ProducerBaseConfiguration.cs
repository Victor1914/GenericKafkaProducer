namespace GenericKafkaProducer.Models.Configurations
{
    public class ProducerBaseConfiguration
    {
        public string Type { get; set; }

        public string Topic { get; set; }

        public string ReadFormat { get; set; }

        public string WriteFormat { get; set; }

        public RuleSetConfiguration RuleSet { get; set; }
    }
}