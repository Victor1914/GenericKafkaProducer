namespace GenericKafkaProducer.Exceptions
{
    using System;
    using System.Collections.Generic;
    using Models.Enums;
    using Models.Errors;
    using Models.Responses;

    public class FileProviderException : BusinessException
    {
        public FileProviderException(StatusType errorType, string filePath, List<string> fileNames, Exception systemException) : base(errorType, systemException)
        {
            FilePath = filePath;
            FileNames = fileNames;
        }

        public string FilePath { get; set; }

        public List<string> FileNames { get; set; }

        public override ProducerResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.Data = new FileProviderError
            {
                FilePath = FilePath,
                FileNames = FileNames
            };

            return response;
        }
    }
}