namespace GenericKafkaProducer.Infrastructure.Serializers.MsgPack
{
    using Confluent.Kafka;
    using Interfaces.Serializers;
    using MessagePack;

    public class MsgPackSerializer<TContract> : ISerializer<TContract>, ICustomSerializer
    {
        public byte[] Serialize(TContract data, SerializationContext context) => MessagePackSerializer.Serialize(data);
    }
}