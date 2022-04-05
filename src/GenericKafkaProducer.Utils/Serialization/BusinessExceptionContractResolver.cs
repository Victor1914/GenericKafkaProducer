namespace GenericKafkaProducer.Utils.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class BusinessExceptionContractResolver : DefaultContractResolver
    {
        private readonly List<string> _excludedProperties = new List<string>
        {
            nameof(Exception.Data),
            nameof(Exception.HelpLink),
            nameof(Exception.HResult),
            nameof(Exception.Source),
            nameof(Exception.StackTrace),
            nameof(Exception.TargetSite)
        };

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var defaultProperties = base.CreateProperties(type, memberSerialization);

            return defaultProperties.Where(p => !_excludedProperties.Contains(p.PropertyName)).ToList();
        }
    }
}