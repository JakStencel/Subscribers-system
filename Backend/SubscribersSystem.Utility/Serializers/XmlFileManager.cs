using SubscribersSystem.Business.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SubscribersSystem.Utility.Serializers
{
    public class XmlFileManager : ISerializer
    {
        public async Task<string> SaveToFileAsync(string filePath, InvoiceBl invoice)
        {

            return await Task.Run(() =>
                       {
                           XmlSerializer serializer = new XmlSerializer(typeof(InvoiceBl));

                           using (var write = new StreamWriter(filePath))
                           {
                               serializer.Serialize(write, invoice);
                           }

                           using (var textWriter = new StringWriter())
                           {
                               serializer.Serialize(textWriter, invoice);
                               return textWriter.ToString();
                           }
                       });
        }
    }
}
