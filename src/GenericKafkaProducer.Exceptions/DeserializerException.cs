namespace GenericKafkaProducer.Exceptions
{
    using System;
    using Models.Enums;
    using Models.Errors;
    using Models.Responses;

    public class DeserializerException : BusinessException
    {
        public DeserializerException(StatusType errorType, string fileName, Exception systemException = null) : base(errorType, systemException)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }

        public override ProducerResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.Data = new ProducerError
            {
                FileName = FileName
            };

            return response;
        }
    }
}