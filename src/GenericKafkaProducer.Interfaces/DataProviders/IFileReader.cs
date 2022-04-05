namespace GenericKafkaProducer.Interfaces.DataProviders
{
    using System.Collections.Generic;
    using Models.Data.Interfaces;

    public interface IDataProvider<TContract>
        where TContract : class
    {
        TContract GetEntity(IBaseRuleSet baseRuleSet);

        List<TContract> GetEntities(IBaseRuleSet baseRuleSet);
    }
}