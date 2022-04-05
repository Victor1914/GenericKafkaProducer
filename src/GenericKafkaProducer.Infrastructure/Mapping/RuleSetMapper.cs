namespace GenericKafkaProducer.Infrastructure.Mapping
{
    using AutoMapper;
    using Interfaces.Mapping;
    using Models.Data;
    using Models.Requests;

    public class RuleSetMapper<TRequest, TRuleSet> : IRuleSetMapper<TRequest>
        where TRequest : ProducerRequest
        where TRuleSet : BaseRuleSet
    {
        private readonly IMapper _mapper;

        public RuleSetMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public BaseRuleSet Map(TRequest request)
        {
            return _mapper.Map<TRequest, TRuleSet>(request);
        }
    }
}
