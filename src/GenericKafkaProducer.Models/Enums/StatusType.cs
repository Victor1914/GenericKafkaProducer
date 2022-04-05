namespace GenericKafkaProducer.Models.Enums
{
    using System.ComponentModel;

    public enum StatusType
    {
        [Description("Data is successfully produced")]
        Success = 1,
        [Description("Producer error")]
        Error = 2,
        [Description("Unable to produce file")]
        FailedToProduce = 3,
        [Description("Failed preparing file")]
        FileProviderFail = 4,
        [Description("Unable to read file")]
        FileReaderFail = 5,
        [Description("Unable to deserialize object")]
        DeserializerFail = 6,
        [Description("Folder not provided")]
        FolderNotProvided = 7,
        [Description("File not provided")]
        FileNotProvided = 8,
        [Description("Folder not found")]
        FolderNotFound = 9,
        [Description("File with name {0} not found")]
        FileNotFound = 10,
        [Description("Invalid items count")]
        InvalidItemsCount = 11,
        [Description("Type not provided")]
        TypeNotProvided = 12,
        [Description("Request validation error")]
        RequestValidationError = 13
    }
}