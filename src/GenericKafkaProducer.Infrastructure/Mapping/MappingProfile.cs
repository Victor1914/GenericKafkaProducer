namespace GenericKafkaProducer.Infrastructure.Mapping
{
    using AutoMapper;
    using Models.Data;
    using Models.Requests;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FileProducerRequest, FileProviderRuleSet>();
            CreateMap<MockProducerRequest, MockProviderRuleSet>();
        }
    }
}
