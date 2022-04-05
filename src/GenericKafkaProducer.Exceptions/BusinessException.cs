namespace GenericKafkaProducer.Exceptions
{
    using System;
    using Models.Enums;
    using Models.Responses;
    using Utils.Extensions;

    public class BusinessException : BaseException
    {
        protected BusinessException(StatusType errorType, Exception systemException = null) : base(systemException)
        {
            StatusCode = (int)errorType;
            Message = errorType.GetDescription();
        }

        public int StatusCode { get; }

        public override string Message { get; }

        public override ProducerResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.StatusCode = StatusCode;
            response.Message = Message;

            return response;
        }
    }
}