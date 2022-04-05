namespace GenericKafkaProducer.Infrastructure.Kafka
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Exceptions;

    using Interfaces.Kafka;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using Utils.Extensions;

    public class KafkaProducerStartup : BackgroundService
    {
        private readonly ILogger<KafkaProducerStartup> _logger;
        private readonly IEnumerable<IKafkaProducer> _producers;

        public KafkaProducerStartup(IEnumerable<IKafkaProducer> producers, ILogger<KafkaProducerStartup> logger)
        {
            _producers = producers;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_producers.Any())
                return;

            await _producers.AsyncParallelForEach(async producer =>
            {
                try
                {
                    await producer.Produce();
                    _logger.LogInformation($"Initial data from producer {producer.GetType()} is successfully produced.");
                }
                catch (BusinessException ex)
                {
                    _logger.DumpExceptionInfo(ex);
                }
                catch (Exception ex)
                {
                    _logger.DumpExceptionInfo(ex);
                }
            }, _producers.Count());
        }
    }
}