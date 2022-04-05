namespace GenericKafkaProducer.Infrastructure.Serializers.JSON
{
    using System.Text;
    using Confluent.Kafka;
    using Interfaces.Serializers;
    using Newtonsoft.Json;

    public class JsonSerializer<TContract> : ISerializer<TContract>, ICustomSerializer
    {
        public byte[] Serialize(TContract data, SerializationContext context) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
}