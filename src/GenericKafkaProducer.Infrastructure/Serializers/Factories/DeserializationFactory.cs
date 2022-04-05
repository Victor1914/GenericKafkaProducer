namespace GenericKafkaProducer.Infrastructure.Serializers.Factories
{
    using System.Collections.Generic;
    using Confluent.Kafka;
    using Core;
    using Interfaces.Serializers;

    public class DeserializationFactory : BaseFactory<ICustomDeserializer>, IDeserializationFactory
    {
        public DeserializationFactory(IEnumerable<ICustomDeserializer> deserializers) : base(deserializers) { }

        public IDeserializer<TContract> GetDeserializer<TContract>(string format) where TContract : class => (IDeserializer<TContract>)GetEntity<TContract>($"{format}Deserializer");
    }
}
