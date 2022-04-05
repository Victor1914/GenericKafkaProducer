namespace GenericKafkaProducer.Models.Requests
{
    using System.Collections.Generic;

    public class FileProducerRequest : ProducerRequest
    {
        public string FolderName { get; set; }

        public List<string> FileNames { get; set; }
    }
}