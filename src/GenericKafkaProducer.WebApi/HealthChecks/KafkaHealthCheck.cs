namespace GenericKafkaProducer.WebApi.HealthChecks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Options;
    using Models.Configurations;

    public class KafkaHealthCheck : IHealthCheck
    {
        private IProducer<string, string> _producer;
        private readonly IOptions<KafkaConfiguration> _kafkaConfig;

        public KafkaHealthCheck(IOptions<KafkaConfiguration> kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                _producer ??= new ProducerBuilder<string, string>(_kafkaConfig.Value.GetProducerConfig()).Build();

                var message = new Message<string, string>
                {
                    Key = "healthcheck-key",
                    Value = $"Check Kafka healthy on {DateTime.UtcNow}"
                };

                var result = await _producer.ProduceAsync("healthcheck-topic", message, cancellationToken);

                return result.Status == PersistenceStatus.NotPersisted
                    ? HealthCheckResult.Unhealthy("Kafka failed.")
                    : HealthCheckResult.Healthy("Kafka is healthy.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}
