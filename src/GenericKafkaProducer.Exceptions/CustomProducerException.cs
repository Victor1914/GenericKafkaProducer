namespace GenericKafkaProducer.Exceptions
{
    using System;
    using Models.Enums;
    using Models.Errors;
    using Models.Responses;

    public class CustomProducerException : BusinessException
    {
        public CustomProducerException(StatusType errorType, string topic = null, string fileName = null, Exception systemException = null)
            : base(errorType, systemException)
        {
            Topic = topic;
            FileName = fileName;
        }

        public string Topic { get; set; }

        public string FileName { get; set; }

        public override ProducerResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.Data = new ProducerError
            {
                Topic = Topic,
                FileName = FileName
            };

            return response;
        }
    }
}