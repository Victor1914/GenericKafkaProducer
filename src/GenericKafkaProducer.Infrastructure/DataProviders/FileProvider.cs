namespace GenericKafkaProducer.Infrastructure.DataProviders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Confluent.Kafka;
    using Exceptions;
    using Extensions;
    using Interfaces.DataProviders;
    using Interfaces.Serializers;
    using Microsoft.Extensions.Options;
    using Models.Configurations;
    using Models.Data;
    using Models.Data.Interfaces;
    using Models.Enums;
    using Utils.Extensions;
    using Utils.Helpers;

    public class FileProvider<TContract> : IDataProvider<TContract>
        where TContract : class
    {
        private readonly FileProviderRuleSet _fileProviderRuleSet;
        private readonly IDeserializationFactory _deserializationFactory;

        public FileProvider(IOptions<ProducerConfiguration> producerConfig, IDeserializationFactory deserializationFactory)
        {
            _fileProviderRuleSet = producerConfig.Value.GetFileProviderRuleSet<TContract>();
            _deserializationFactory = deserializationFactory;
        }

        public TContract GetEntity(IBaseRuleSet baseRuleSet)
        {
            return GetEntities(baseRuleSet).FirstOrDefault();
        }

        public List<TContract> GetEntities(IBaseRuleSet baseRuleSet)
        {
            var ruleSet = baseRuleSet == null
                ? _fileProviderRuleSet
                : (FileProviderRuleSet)baseRuleSet;

            var directory = PathUtils.GetMocksDirectoryName(ruleSet.FolderName);
            var filesToDeserialize = RetrieveFiles(ruleSet, directory);
            var deserializer = _deserializationFactory.GetDeserializer<TContract>(ruleSet.ReadFormat ?? _fileProviderRuleSet.ReadFormat);

            return CreateEntities(filesToDeserialize, deserializer, directory);
        }

        private static IEnumerable<string> RetrieveFiles(FileProviderRuleSet ruleSet, string directory)
        {
            IEnumerable<string> result;

            try
            {
                var files = new DirectoryInfo(directory)
                    .EnumerateFiles("*.*")
                    .EmptyIfNull();

                if (!ruleSet.FileNames.IsNullOrEmpty()) //load specific files
                {
                    files = files
                        .Where(file => ruleSet.FileNames.Contains(Path.GetFileNameWithoutExtension(file.Name)));
                }

                result = files.Select(file => file.Name).ToList();
            }
            catch (Exception ex)
            {
                throw new FileProviderException(StatusType.FileProviderFail, ruleSet.FolderName, ruleSet.FileNames, ex);
            }

            return result;
        }

        private static List<TContract> CreateEntities(IEnumerable<string> filesToDeserialize, IDeserializer<TContract> deserializer, string directory)
        {
            var result = new List<TContract>();

            foreach (var fileName in filesToDeserialize)
            {
                var rawFile = ReadFile(directory, fileName);
                result.Add(deserializer.SafeDeserialize(rawFile, fileName));
            }

            return result;
        }

        private static byte[] ReadFile(string directory, string fileName)
        {
            var path = string.Empty;

            try
            {
                path = Path.Combine(directory, fileName);

                return File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                throw new ReadAllBytesException(StatusType.FileReaderFail, path, fileName, ex);
            }
        }
    }
}