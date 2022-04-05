namespace GenericKafkaProducer.Utils.Extensions
{
    using System;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Serialization;

    public static class LoggerExtensions
    {
        public static void DumpExceptionInfo<TCategoryName>(this ILogger<TCategoryName> logger, Exception exception)
        {
            var serializedException = JsonConvert.SerializeObject(exception, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new BusinessExceptionContractResolver()
            });

            logger.LogError(exception, serializedException);
        }
    }
}