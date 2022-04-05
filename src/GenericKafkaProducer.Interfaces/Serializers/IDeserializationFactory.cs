namespace GenericKafkaProducer.Interfaces.Serializers
{
    using Confluent.Kafka;

    public interface IDeserializationFactory
    {
        IDeserializer<TContract> GetDeserializer<TContract>(string format) where TContract : class;
    }
}
