namespace GenericKafkaProducer.Infrastructure.Serializers.JSON
{
    using System;
    using System.Text;
    using Confluent.Kafka;
    using Interfaces.Serializers;
    using Newtonsoft.Json;

    public class JsonDeserializer<TContract> : IDeserializer<TContract>, ICustomDeserializer
    {
        public TContract Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) => isNull ? default : JsonConvert.DeserializeObject<TContract>(Encoding.UTF8.GetString(data));
    }
}