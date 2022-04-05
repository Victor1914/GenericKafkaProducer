namespace GenericKafkaProducer.Models.Errors
{
    using System.Collections.Generic;

    public class FileProviderError
    {
        public string FilePath { get; set; }

        public List<string> FileNames { get; set; }
    }
}