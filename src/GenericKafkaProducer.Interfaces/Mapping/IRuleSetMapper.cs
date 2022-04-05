namespace GenericKafkaProducer.Interfaces.Mapping
{
    using Models.Data;
    using Models.Requests;

    public interface IRuleSetMapper<in TRequest>
        where TRequest : ProducerRequest
    {
        BaseRuleSet Map(TRequest request);
    }
}