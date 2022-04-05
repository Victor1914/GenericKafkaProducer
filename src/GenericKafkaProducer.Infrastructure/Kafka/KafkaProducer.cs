namespace GenericKafkaProducer.Infrastructure.Kafka
{
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Extensions;
    using Interfaces.DataProviders;
    using Interfaces.Kafka;
    using Interfaces.Serializers;
    using Microsoft.Extensions.Options;
    using Models.Configurations;
    using Models.Data.Interfaces;
    using Utils.Extensions;

    public class KafkaProducer<TContract> : IKafkaProducer
        where TContract : class
    {
        private readonly ProducerBaseConfiguration _producerConfig;
        private readonly IOptions<KafkaConfiguration> _kafkaConfig;

        private readonly IDataProvider<TContract> _dataProvider;
        private readonly ISerializationFactory _serializationFactory;

        public KafkaProducer(
            IOptions<ProducerConfiguration> producerConfig,
            IOptions<KafkaConfiguration> kafkaConfig,
            IDataProvider<TContract> dataProvider,
            ISerializationFactory serializationFactory)
        {
            _producerConfig = producerConfig.Value.GetConfiguration<TContract>();
            _kafkaConfig = kafkaConfig;

            _dataProvider = dataProvider;
            _serializationFactory = serializationFactory;
        }

        public async Task Produce(IBaseRuleSet baseRuleSet = null)
        {
            var filesToProduce = _dataProvider.GetEntities(baseRuleSet);

            using var producer = BuildProducer(baseRuleSet?.WriteFormat ?? _producerConfig.WriteFormat);
            foreach (var file in filesToProduce)
            {
                await producer.SafeProduceAsync(file, _producerConfig.Topic);
            }
        }

        private IProducer<string, TContract> BuildProducer(string format)
        {
            var serializer = _serializationFactory.GetSerializer<TContract>(format);

            return new ProducerBuilder<string, TContract>(_kafkaConfig.Value.GetProducerConfig())
                .SetValueSerializer(serializer)
                .Build();
        }
    }
}