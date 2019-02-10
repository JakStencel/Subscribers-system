using SubscribersSystem.Business.Models;
using System.Threading.Tasks;

namespace SubscribersSystem.Utility.Serializers
{
    public interface ISerializer
    {
        Task<string> SaveToFileAsync(string filePath, InvoiceBl invoice);
    }
}
