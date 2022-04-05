namespace GenericKafkaProducer.Infrastructure.Kafka
{
    using System;
    using System.Threading.Tasks;
    using Exceptions;
    using Interfaces.Kafka;
    using Interfaces.Mapping;
    using Interfaces.Validation;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Models.Requests;
    using Models.Responses.Interfaces;
    using Utils.Extensions;
    using Utils.Helpers;

    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly IKafkaProducerFactory _kafkaProducerFactory;

        public KafkaProducerService(IServiceProvider serviceProvider, ILogger<KafkaProducerService> logger, IKafkaProducerFactory kafkaProducerFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _kafkaProducerFactory = kafkaProducerFactory;
        }

        public async Task<IProducerResponse> Send<TRequest>(TRequest request)
            where TRequest : ProducerRequest
        {
            if (GetValidator<TRequest>().HasErrors(request, out var errors))
                return ProducerResponseBuilder.ErrorResponse(errors);

            var ruleSet = GetMapper<TRequest>().Map(request);

            try
            {
                await _kafkaProducerFactory
                    .GetProducer(request.Type)
                    .Produce(ruleSet);

                return ProducerResponseBuilder.SuccessResponse();
            }
            catch (BusinessException ex)
            {
                _logger.DumpExceptionInfo(ex);
                return ex.CreateResponse();
            }
            catch (Exception ex)
            {
                _logger.DumpExceptionInfo(ex);
                return ProducerResponseBuilder.ErrorResponse(ex);
            }
        }

        private IRequestValidator<TRequest> GetValidator<TRequest>()
            where TRequest : ProducerRequest
        {
            return _serviceProvider.GetService<IRequestValidator<TRequest>>();
        }

        private IRuleSetMapper<TRequest> GetMapper<TRequest>()
            where TRequest : ProducerRequest
        {
            return _serviceProvider.GetService<IRuleSetMapper<TRequest>>();
        }
    }
}
