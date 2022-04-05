namespace GenericKafkaProducer.Infrastructure.DataProviders
{
    using System.Collections.Generic;
    using Interfaces.DataProviders;
    using Microsoft.Extensions.Options;
    using Models.Configurations;
    using Models.Data;
    using Models.Data.Interfaces;
    using Utils.Extensions;

    public class MockProvider<TContract> : IDataProvider<TContract>
        where TContract : class
    {
        private readonly MockProviderRuleSet _mockProviderRuleSet;
        private readonly IMocker<TContract> _mocker;

        public MockProvider(IOptions<ProducerConfiguration> producerConfig, IMocker<TContract> mocker)
        {
            _mocker = mocker;
            _mockProviderRuleSet = producerConfig.Value.GetMockProviderRuleSet<TContract>();
        }

        public TContract GetEntity(IBaseRuleSet baseRuleSet)
        {
            return _mocker.CreateMock();
        }

        public List<TContract> GetEntities(IBaseRuleSet baseRuleSet)
        {
            var ruleSet = baseRuleSet == null
                ? _mockProviderRuleSet
                : (MockProviderRuleSet)baseRuleSet;

            return _mocker.CreateMocks(ruleSet.ItemsCount);
        }
    }
}