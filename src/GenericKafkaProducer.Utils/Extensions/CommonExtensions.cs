namespace GenericKafkaProducer.Utils.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public static class CommonExtensions
    {
        public static string GetDescription(this Enum enumMember)
        {
            return enumMember
                       .GetType()
                       .GetMember(enumMember.ToString())
                       .FirstOrDefault()
                       ?.GetCustomAttribute<DescriptionAttribute>()
                       ?.Description
                   ?? enumMember.ToString();
        }
    }
}