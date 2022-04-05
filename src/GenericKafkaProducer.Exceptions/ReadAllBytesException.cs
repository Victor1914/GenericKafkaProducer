namespace GenericKafkaProducer.Exceptions
{
    using System;
    using Models.Enums;
    using Models.Responses;

    public class ReadAllBytesException : BusinessException
    {
        public ReadAllBytesException(StatusType errorType, string filePath, string fileName, Exception systemException) : base(errorType, systemException)
        {
            FilePath = filePath;
            FileName = fileName;
        }

        public string FilePath { get; set; }

        public string FileName { get; set; }

        public override ProducerResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.Data = $"Unable to load file [{FileName}] from path [{FilePath}]";

            return response;
        }
    }
}