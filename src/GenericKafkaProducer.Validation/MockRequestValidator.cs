namespace GenericKafkaProducer.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.Data;
    using Models.Enums;
    using Models.Requests;
    using Utils.Extensions;

    public class MockRequestValidator<TRequest> : BaseRequestValidator<TRequest>
        where TRequest : MockProducerRequest
    {
        public override bool HasErrors(TRequest mockProducerRequest, out List<ValidationError> errors)
        {
            base.HasErrors(mockProducerRequest, out errors);

            if (mockProducerRequest.ItemsCount < 1)
                errors.Add(new ValidationError { StatusCode = (int)StatusType.InvalidItemsCount, Message = StatusType.InvalidItemsCount.GetDescription() });

            return errors.Any();
        }
    }
}