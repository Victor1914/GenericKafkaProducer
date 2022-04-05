namespace GenericKafkaProducer.Infrastructure.Kafka
{
    using System.Collections.Generic;
    using Core;
    using Interfaces.Kafka;

    public class KafkaProducerFactory : BaseFactory<IKafkaProducer>, IKafkaProducerFactory
    {
        public KafkaProducerFactory(IEnumerable<IKafkaProducer> producers) : base(producers) { }

        public IKafkaProducer GetProducer(string contract) => GetEntity(contract, "KafkaProducer");
    }
}
