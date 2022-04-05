namespace GenericKafkaProducer.WebApi.Behaviour
{
    using System;

    using AutoMapper;

    using HealthChecks;
    using Infrastructure.Kafka;
    using Infrastructure.Mapping;
    using Infrastructure.Serializers.Factories;
    using Infrastructure.Serializers.JSON;
    using Infrastructure.Serializers.MsgPack;
    using Interfaces.Kafka;
    using Interfaces.Mapping;
    using Interfaces.Serializers;
    using Interfaces.Validation;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Configurations;
    using Models.Data;
    using Models.Requests;
    using Validation;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services
               .AddSingleton<KafkaHealthCheck>()
               .AddHealthChecks()
               .AddCheck<KafkaHealthCheck>(nameof(KafkaHealthCheck), timeout: TimeSpan.FromMinutes(1));

            return services;
        }

        public static IServiceCollection AddSingalR(this IServiceCollection services)
        {
            services
                .AddSignalR(hubOptions =>
                {
                    hubOptions.EnableDetailedErrors = true;
                    hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
                })
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });

            return services;
        }

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(MappingProfile))
                .AddSingleton<IRuleSetMapper<FileProducerRequest>, RuleSetMapper<FileProducerRequest, FileProviderRuleSet>>()
                .AddSingleton<IRuleSetMapper<MockProducerRequest>, RuleSetMapper<MockProducerRequest, MockProviderRuleSet>>();
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            return services
                .AddSingleton<IRequestValidator<FileProducerRequest>, FileRequestValidator<FileProducerRequest>>()
                .AddSingleton<IRequestValidator<MockProducerRequest>, MockRequestValidator<MockProducerRequest>>();
        }

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<KafkaConfiguration>(config.GetSection("Kafka"))
                .Configure<ProducerConfiguration>(config.GetSection("Producer"));
        }

        public static IServiceCollection AddKafkaProducers(this IServiceCollection services)
        {
            return services
                .AddHostedService<KafkaProducerStartup>()
                .AddSingleton<IKafkaProducerFactory, KafkaProducerFactory>()
                .AddSingleton<IKafkaProducerService, KafkaProducerService>();
            // Add  KafkaProducer of the wanted type    
            //.AddSingleton<IKafkaProducer, KafkaProducer<Type>>()
        }

        public static IServiceCollection AddFileDataProvider(this IServiceCollection services)
        {
            return services;
            //Used for files
            //Add FileProvider of the wanted type as IDataProvider
            //.AddSingleton<IDataProvider<Type>, FileProvider<Type>>()
        }

        public static IServiceCollection AddMockDataProvider(this IServiceCollection services)
        {
            return services;
            //Used for mocked files
            //Add MockProvider of the wanted type as IDataProvider and Mocker of the type
            //.AddSingleton<IDataProvider<Type>, MockProvider<Type>>()
            //.AddSingleton<IMocker<Type>, TypeMocker>();
        }

        public static IServiceCollection AddSerializers(this IServiceCollection services)
        {
            return services
                .AddSerializationFactories();
            //Add AddSerialization of the needed type
            //.AddSerialization<Type>()
        }

        private static IServiceCollection AddSerialization<TContract>(this IServiceCollection services)
        {
            return services
                .AddSingleton<ICustomSerializer, JsonSerializer<TContract>>()
                .AddSingleton<ICustomSerializer, MsgPackSerializer<TContract>>()
                .AddSingleton<ICustomDeserializer, JsonDeserializer<TContract>>()
                .AddSingleton<ICustomDeserializer, MsgPackDeserializer<TContract>>();
        }

        private static IServiceCollection AddSerializationFactories(this IServiceCollection services)
        {
            return services
                .AddSingleton<ISerializationFactory, SerializationFactory>()
                .AddSingleton<IDeserializationFactory, DeserializationFactory>();
        }
    }
}