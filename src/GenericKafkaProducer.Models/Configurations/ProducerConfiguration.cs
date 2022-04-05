namespace GenericKafkaProducer.Models.Configurations
{
    using System.Collections.Generic;

    public class ProducerConfiguration
    {
        public ProducerBaseConfiguration Defaults { get; set; }

        public List<ProducerBaseConfiguration> Instances { get; set; }
    }
}