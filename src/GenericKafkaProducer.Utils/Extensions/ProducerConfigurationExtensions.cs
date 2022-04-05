namespace GenericKafkaProducer.Utils.Extensions
{
    using System;
    using System.Linq;
    using Models.Configurations;
    using Models.Data;

    public static class ProducerConfigurationExtensions
    {
        public static ProducerBaseConfiguration GetConfiguration<TContract>(this ProducerConfiguration producerConfig) where TContract : class
        {
            var type = typeof(TContract).Name;
            var matchedConfig = producerConfig.Instances.FirstOrDefault(baseConfig => string.Equals(baseConfig.Type, type, StringComparison.CurrentCultureIgnoreCase));
            var defaultConfig = producerConfig.Defaults;

            if (matchedConfig == null)
                return defaultConfig;

            matchedConfig.ReadFormat ??= defaultConfig.ReadFormat;
            matchedConfig.WriteFormat ??= defaultConfig.WriteFormat;

            if (matchedConfig.RuleSet == null)
            {
                matchedConfig.RuleSet = defaultConfig.RuleSet;
            }
            else
            {
                matchedConfig.RuleSet.ItemsCount = matchedConfig.RuleSet.ItemsCount <= 0
                    ? defaultConfig.RuleSet.ItemsCount
                    : matchedConfig.RuleSet.ItemsCount;

                matchedConfig.RuleSet.Folder = string.IsNullOrEmpty(matchedConfig.RuleSet.Folder)
                    ? defaultConfig.RuleSet.Folder
                    : matchedConfig.RuleSet.Folder;

                matchedConfig.RuleSet.FileNames = matchedConfig.RuleSet.FileNames.IsNullOrEmpty()
                    ? defaultConfig.RuleSet.FileNames
                    : matchedConfig.RuleSet.FileNames;
            }

            return matchedConfig;
        }

        public static FileProviderRuleSet GetFileProviderRuleSet<TContract>(this ProducerConfiguration producerConfig) where TContract : class
        {
            var config = producerConfig.GetConfiguration<TContract>();

            return new FileProviderRuleSet
            {
                ReadFormat = config.ReadFormat,
                WriteFormat = config.WriteFormat,
                FolderName = config.RuleSet.Folder,
                FileNames = config.RuleSet.FileNames
            };
        }

        public static MockProviderRuleSet GetMockProviderRuleSet<TContract>(this ProducerConfiguration producerConfig) where TContract : class
        {
            var config = producerConfig.GetConfiguration<TContract>();

            return new MockProviderRuleSet
            {
                ReadFormat = config.ReadFormat,
                WriteFormat = config.WriteFormat,
                ItemsCount = config.RuleSet.ItemsCount
            };
        }
    }
}