namespace GenericKafkaProducer.Interfaces.Kafka
{
    using System.Threading.Tasks;
    using Models.Data.Interfaces;

    public interface IKafkaProducer
    {
        Task Produce(IBaseRuleSet baseRuleSet = null);
    }
}