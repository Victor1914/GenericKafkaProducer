namespace GenericKafkaProducer.Models.Configurations
{
    using System.Collections.Generic;

    public class RuleSetConfiguration
    {
        public string Folder { get; set; }

        public List<string> FileNames { get; set; }

        public int ItemsCount { get; set; }
    }
}