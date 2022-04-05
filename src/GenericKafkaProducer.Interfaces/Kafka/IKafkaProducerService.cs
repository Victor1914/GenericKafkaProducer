namespace GenericKafkaProducer.Interfaces.Kafka
{
    using System.Threading.Tasks;
    using Models.Requests;
    using Models.Responses.Interfaces;

    public interface IKafkaProducerService
    {
        Task<IProducerResponse> Send<TRequest>(TRequest request) where TRequest : ProducerRequest;
    }
}
