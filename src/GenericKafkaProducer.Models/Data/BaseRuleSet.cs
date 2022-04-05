namespace GenericKafkaProducer.Models.Data
{
    using Interfaces;

    public class BaseRuleSet : IBaseRuleSet
    {
        public string ReadFormat { get; set; }

        public string WriteFormat { get; set; }
    }
}