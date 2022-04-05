namespace GenericKafkaProducer.Models.Responses
{
    using System.Collections.Generic;
    using Data;

    public class ProducerValidationResponse : ProducerBaseResponse
    {
        public List<ValidationError> ValidationErrors { get; set; }
    }
}