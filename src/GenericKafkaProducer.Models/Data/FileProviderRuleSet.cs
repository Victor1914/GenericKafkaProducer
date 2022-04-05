namespace GenericKafkaProducer.Models.Data
{
    using System.Collections.Generic;

    public class FileProviderRuleSet : BaseRuleSet
    {
        public string FolderName { get; set; }

        public List<string> FileNames { get; set; }
    }
}