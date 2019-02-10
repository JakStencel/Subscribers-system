using Newtonsoft.Json;
using SubscribersSystem.Business.Models;
using System.IO;
using System.Threading.Tasks;

namespace SubscribersSystem.Utility.Serializers
{
    public class JsonFileManager : ISerializer
    {
        public async Task<string> SaveToFileAsync(string filePath, InvoiceBl invoice)
        {
           return await Task.Run<string>(() =>
           {
               string json = JsonConvert.SerializeObject(invoice, Formatting.Indented);
               File.WriteAllText((filePath), json);
               return json;
           });

        }
    }
}
