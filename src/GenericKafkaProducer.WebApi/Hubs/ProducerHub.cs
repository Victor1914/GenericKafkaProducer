namespace GenericKafkaProducer.WebApi.Hubs
{
    using System.Threading.Tasks;
    using Interfaces.Kafka;
    using Microsoft.AspNetCore.SignalR;
    using Models.Requests;
    using Models.Responses.Interfaces;

    public class ProducerHub : Hub
    {
        private readonly IKafkaProducerService _kafkaProducerService;

        public ProducerHub(IKafkaProducerService kafkaProducerService)
        {
            _kafkaProducerService = kafkaProducerService;
        }

        public async Task<IProducerResponse> ProduceFiles(FileProducerRequest request)
        {
            return await _kafkaProducerService.Send(request);
        }

        public async Task<IProducerResponse> ProduceMocks(MockProducerRequest request)
        {
            return await _kafkaProducerService.Send(request);
        }
    }
}