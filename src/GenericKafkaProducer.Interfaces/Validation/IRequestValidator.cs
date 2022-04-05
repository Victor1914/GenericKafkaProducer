namespace GenericKafkaProducer.Interfaces.Validation
{
    using System.Collections.Generic;
    using Models.Data;
    using Models.Requests;

    public interface IRequestValidator<in TRequest>
        where TRequest : ProducerRequest
    {
        bool HasErrors(TRequest request, out List<ValidationError> errors);
    }
}
