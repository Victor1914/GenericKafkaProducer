namespace GenericKafkaProducer.Interfaces.Kafka
{
    public interface IKafkaProducerFactory
    {
        IKafkaProducer GetProducer(string contract);
    }
}
