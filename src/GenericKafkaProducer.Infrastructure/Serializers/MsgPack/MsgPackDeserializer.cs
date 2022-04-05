namespace GenericKafkaProducer.Infrastructure.Serializers.MsgPack
{
    using System;
    using System.IO;
    using Confluent.Kafka;
    using Interfaces.Serializers;
    using MessagePack;

    public class MsgPackDeserializer<TContract> : IDeserializer<TContract>, ICustomDeserializer
    {
        public TContract Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
                return default;

            using var byteStream = new MemoryStream(data.ToArray());
            return MessagePackSerializer.Deserialize<TContract>(byteStream);
        }
    }
}