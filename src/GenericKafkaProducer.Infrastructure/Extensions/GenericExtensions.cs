namespace GenericKafkaProducer.Infrastructure.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Exceptions;
    using Models.Enums;

    public static class GenericExtensions
    {
        public static async Task<DeliveryResult<TKey, TValue>> SafeProduceAsync<TKey, TValue>(this IProducer<TKey, TValue> producer, TValue file, string topic)
        {
            try
            {
                return await producer.ProduceAsync(topic, new Message<TKey, TValue> { Value = file });
            }
            catch (Exception ex)
            {
                throw new CustomProducerException(StatusType.FailedToProduce, topic, nameof(file), ex);
            }
        }

        public static TItem SafeDeserialize<TItem>(this IDeserializer<TItem> deserializer, byte[] rawFile, string fileName, bool isNull = false)
        {
            try
            {
                return deserializer.Deserialize(rawFile, isNull, SerializationContext.Empty);
            }
            catch (Exception ex)
            {
                throw new DeserializerException(StatusType.DeserializerFail, fileName, ex);
            }
        }
    }
}
