namespace GenericKafkaProducer.Interfaces.Serializers
{
    using Confluent.Kafka;

    public interface ISerializationFactory
    {
        ISerializer<TContract> GetSerializer<TContract>(string format) where TContract : class;
    }
}