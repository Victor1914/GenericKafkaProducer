namespace GenericKafkaProducer.Models.Data.Interfaces
{
    public interface IBaseRuleSet
    {
        string ReadFormat { get; set; }

        string WriteFormat { get; set; }
    }
}