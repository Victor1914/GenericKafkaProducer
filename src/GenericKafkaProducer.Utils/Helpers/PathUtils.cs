namespace GenericKafkaProducer.Utils.Helpers
{
    using System.IO;
    using System.Reflection;

    public static class PathUtils
    {
        public static string GetMocksDirectoryName(string folderName)
        {
            var separator = Path.DirectorySeparatorChar;
            
            return $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{separator}MockedFiles{separator}{folderName}";
        }
    }
}