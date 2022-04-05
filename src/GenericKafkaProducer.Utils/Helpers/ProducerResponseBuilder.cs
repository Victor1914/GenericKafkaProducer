namespace GenericKafkaProducer.Utils.Helpers
{
    using System;
    using System.Collections.Generic;
    using Extensions;
    using Models.Data;
    using Models.Enums;
    using Models.Errors;
    using Models.Responses;

    public static class ProducerResponseBuilder
    {
        public static ProducerValidationResponse ErrorResponse(List<ValidationError> errors)
        {
            return new ProducerValidationResponse
            {
                StatusCode = (int)StatusType.RequestValidationError,
                Message = StatusType.RequestValidationError.GetDescription(),
                ValidationErrors = errors
            };
        }

        public static ProducerResponse ErrorResponse(Exception ex)
        {
            return new ProducerResponse
            {
                StatusCode = (int)StatusType.Error,
                Message = StatusType.Error.GetDescription(),
                SystemError = new SystemError { SystemException = ex }
            };
        }

        public static ProducerResponse SuccessResponse()
        {
            return new ProducerResponse
            {
                StatusCode = (int)StatusType.Success,
                Message = StatusType.Success.GetDescription()
            };
        }
    }
}
