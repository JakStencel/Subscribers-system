using SubscribersSystem.Business.Models;
using SubscribersSystem.Utility.Serializers;
using System;
using System.IO;

namespace SubscribersSystem.Utility
{
    public interface ISerializerProvider
    {
        ISerializer SerializerChanger(string serializer);
        string GetCurrentDirectory(string serializerExtension, InvoiceBl invoice);
    }

    public class SerializerProvider : ISerializerProvider
    {
        public ISerializer SerializerChanger(string serializer)
        {
            switch (serializer.ToLower())
            {
                case "xml":
                    return new XmlFileManager();
                case "json":
                    return new JsonFileManager();
                default:
                    Console.WriteLine("Invalid format of serializer");
                    return null;
            }
        }

        public string GetCurrentDirectory(string serializerExtension, InvoiceBl invoice)
        {
            var filePath = Directory.GetCurrentDirectory();

            return Path.Combine(filePath, $"{invoice.Number}.{serializerExtension.ToLowerInvariant()}");
        }
}
}
