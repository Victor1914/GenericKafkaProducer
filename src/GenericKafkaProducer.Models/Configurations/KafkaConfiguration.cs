namespace GenericKafkaProducer.Models.Configurations
{
    using Confluent.Kafka;

    public class KafkaConfiguration
    {
        public string BootstrapServer { get; set; }

        public ProducerConfig GetProducerConfig() =>
            new ProducerConfig
            {
                BootstrapServers = BootstrapServer
            };
    }
}
