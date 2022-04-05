namespace GenericKafkaProducer.Infrastructure.Serializers.Factories
{
    using System.Collections.Generic;
    using Confluent.Kafka;
    using Core;
    using Interfaces.Serializers;

    public class SerializationFactory : BaseFactory<ICustomSerializer>, ISerializationFactory
    {
        public SerializationFactory(IEnumerable<ICustomSerializer> serializers) : base(serializers) { }

        public ISerializer<TContract> GetSerializer<TContract>(string format) where TContract : class => (ISerializer<TContract>)GetEntity<TContract>($"{format}Serializer");
    }
}
