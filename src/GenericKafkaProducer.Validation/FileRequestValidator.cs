namespace GenericKafkaProducer.Validation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Models.Data;
    using Models.Enums;
    using Models.Requests;
    using Utils.Extensions;

    public class FileRequestValidator<TRequest> : BaseRequestValidator<TRequest>
        where TRequest : FileProducerRequest
    {
        public override bool HasErrors(TRequest fileProducerRequest, out List<ValidationError> errors)
        {
            base.HasErrors(fileProducerRequest, out errors);

            if (string.IsNullOrEmpty(fileProducerRequest.FolderName))
                errors.Add(new ValidationError { StatusCode = (int)StatusType.FolderNotProvided, Message = StatusType.FolderNotProvided.GetDescription() });

            if (errors.Any())
                return true;

            if (!Directory.Exists(fileProducerRequest.FolderName))
                errors.Add(new ValidationError { StatusCode = (int)StatusType.FolderNotFound, Message = StatusType.FolderNotFound.GetDescription() });

            CheckIfFilesExist(fileProducerRequest.FileNames, fileProducerRequest.FolderName, ref errors);

            return errors.Any();
        }

        private static void CheckIfFilesExist(IReadOnlyCollection<string> fileNames, string folderName, ref List<ValidationError> errors)
        {
            if (fileNames.IsNullOrEmpty())
                return;

            var files = Directory.GetFiles(folderName)
                .EmptyIfNull()
                .ToList();

            foreach (var fileName in fileNames)
            {
                if (!files.Any(x => x.Contains(fileName)))
                    errors.Add(new ValidationError { StatusCode = (int)StatusType.FileNotFound, Message = string.Format(StatusType.FileNotFound.GetDescription(), fileName) });
            }
        }
    }
}