namespace GenericKafkaProducer.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Validation;
    using Models.Data;
    using Models.Enums;
    using Models.Requests;
    using Utils.Extensions;

    public abstract class BaseRequestValidator<TRequest> : IRequestValidator<TRequest>
        where TRequest : ProducerRequest
    {
        public virtual bool HasErrors(TRequest request, out List<ValidationError> errors)
        {
            errors = new List<ValidationError>();

            if (string.IsNullOrEmpty(request.Type))
                errors.Add(new ValidationError { StatusCode = (int)StatusType.TypeNotProvided, Message = StatusType.TypeNotProvided.GetDescription() });

            return errors.Any();
        }
    }
}